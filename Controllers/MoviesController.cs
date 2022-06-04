using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        public ActionResult Index()
        {
            var movies = new List<Movie> { 
                new Movie { Name = "Shrek!" },
                new Movie { Name = "Abominable Snowman"}
            };
            return View(movies);
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
            // return Content("Hello World");

            // return HttpNotFound();

            // return new EmptyResult();

            return RedirectToAction("Index", "Home", new { page = 1, sortBy = "name"});
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