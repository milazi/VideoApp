using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController() {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.CanManageMovies)) {
                return View("List");
            }

            return View("ReadOnlyList");
        }
        [Authorize(Roles = RoleName.CanManageMovies)]
         public ActionResult Create()
        { 
            var genres = _context.Genres.ToList();
            var viewModel = new MovieFormViewModel { 
                Genres = genres
            };
            return View("MovieForm",viewModel);
        }
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Update(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();

            var genres = _context.Genres.ToList();
            
            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genres = genres
            };
            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(Movie movie)
        {
            try
            {
                if (movie.Id == 0)
                {
                    movie.DateAdded = DateTime.Now;
                    _context.Movies.Add(movie);
                }
                else
                {
                    var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
                    movieInDb.Name = movie.Name;
                    movieInDb.GenreId = movie.GenreId;
                    movieInDb.NumberInStock = movie.NumberInStock;
                    movieInDb.ReleaseDate = movie.ReleaseDate;
                }
                _context.SaveChanges();

                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
               Console.WriteLine(e.Message);
                //return Content(e.ToString());
                //return View("MovieForm");
                return RedirectToAction("Index");
            }
        }
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(c => c.Genre).SingleOrDefault(c => c.Id == id);
            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }
        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };
            var customers = new List<Customer>
            {
                new Customer() { Name ="Customer 1"},
                new Customer() { Name ="Customer 2"}
            };

            var viewModel = new RandomMovieViewModel
            {   
                Movie = movie,
                Customers = customers
            };
            //ViewBag.Movie = movie;
            //  return View(movie);

            return View(viewModel);
        }

        public ActionResult Edit (int id)
        {
            return Content("id=" + id);
        }
        
        //MOVIES
        /*
        public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
                pageIndex = 1;

            if(String.IsNullOrWhiteSpace(sortBy))
                sortBy = "name";

            return Content(String.Format("pageIndex={0}&sortby={1}", pageIndex, sortBy));
        }
        */
        [Route("movies/released/{year}/{month}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }
    }
}