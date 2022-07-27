using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Vidly.Models;
using Vidly.Dtos;
using AutoMapper;

namespace Vidly.Controllers.Api
{
    public class RentalsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Rentals
        public IEnumerable<RentalDto> GetRentals()
        {
            var rentals = db.Rentals.ToList().Select(r => Mapper.Map<Rental, RentalDto>(r));
            return rentals;
        }

        // GET: api/Rentals/5
        public IHttpActionResult GetRental(int id)
        {
            Rental rental = db.Rentals.Find(id);
            if (rental == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Rental,RentalDto>(rental));
        }

        // PUT: api/Rentals/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRental(int id, RentalDto rental)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(Mapper.Map<RentalDto,Rental>(rental)).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Rentals
        [ResponseType(typeof(RentalDto))]
        public IHttpActionResult PostRental(RentalDto rental)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (int movieId in rental.MovieIds) {
                var newRental = Mapper.Map<RentalDto, Rental>(rental);
                newRental.MovieId = movieId;
                newRental.DateRented = DateTime.Now;
                db.Rentals.Add(newRental);
            }
            
            db.SaveChanges();
            
            return Ok(rental);
        }

        // DELETE: api/Rentals/5
        [ResponseType(typeof(RentalDto))]
        public IHttpActionResult DeleteRental(int id)
        {
            Rental rental = db.Rentals.Find(id);
            if (rental == null)
            {
                return NotFound();
            }

            db.Rentals.Remove(rental);
            db.SaveChanges();

            return Ok(Mapper.Map<Rental,RentalDto>(rental));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RentalExists(int id)
        {
            return db.Rentals.Count(e => e.Id == id) > 0;
        }
    }
}