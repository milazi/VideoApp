using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.Dtos;
using AutoMapper;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        ApplicationDbContext _context;

        MoviesController() {
            _context = new ApplicationDbContext();
        }
        // GET: api/Movies
        public IEnumerable<MovieDto> Get()
        {
            var movies = _context.Movies.ToList().Select(Mapper.Map<Movie,MovieDto>);

            return movies;
        }

        // GET: api/Movies/5
        public IHttpActionResult Get(int id)
        {
            var movie = _context.Movies.SingleOrDefault(x => x.Id == id);
            if(movie == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Movie,MovieDto>(movie));
        }

        // POST: api/Movies
        [HttpPost]
        public IHttpActionResult Create([FromBody]MovieDto movieDto)
        {
            if (!ModelState.IsValid) {
                return BadRequest();
            }

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movieDto.Id), movieDto);
        }

        // PUT: api/Movies/5
        [HttpPut]
        public void Update(int id, [FromBody]MovieDto movieDto)
        {
            if (!ModelState.IsValid) 
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var movieInDb = _context.Movies.SingleOrDefault(x => x.Id == id);
            
            if (movieInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Mapper.Map(movieDto, movieInDb);

            _context.SaveChanges();

        }

        // DELETE: api/Movies/5
        public void Delete(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(x => x.Id == id);
            
            if (movieInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();
        }
    }
}
