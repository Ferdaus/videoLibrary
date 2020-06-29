using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using videoLibrary.Models;
using videoLibrary.ViewModels;

namespace videoLibrary.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var genres = _context.Genres.ToList();
            var viewModel = new MovieFormViewModel
            {
               Genres = genres
            };
            return View("MovieForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {

            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    
                    Genres = _context.Genres.ToList()
                };
                return View("MovieForm", viewModel);
            }
            if (movie.Id==0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else 
            {
                var movieInDb = _context.Movies.SingleOrDefault(m => m.Id==movie.Id);

                movieInDb.Name = movie.Name;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
            }    
            
            
               
             _context.SaveChanges();
             return RedirectToAction("Index", "Movies");
            //return View();
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m=> m.Id==id);
            if (movie == null)
                HttpNotFound();
            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = _context.Genres.ToList()
            };
            return View("MovieForm", viewModel);
        }
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek" };

            var customers = new List<Customer>
            {
                new Customer { Name = "Kawsar"},
                new Customer { Name = "Adib"},
                new Customer { Name = "Tauseef"},

            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
            //return View(movie);
            //return new ViewResult();
            //return Content("Hello World");
            //return HttpNotFound();
            //return new EmptyResult();
            //return RedirectToAction("Index", "Home", new { page = 1, sortBy = "name" });
        }

     /*   public ActionResult Edit(int id)
        {
            return Content("id = " + id);
        }  */

        //[Route("movies/released/{year}/{month:regex(\\d{4}): range(1,12)}")]
        [Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1,12)}")]
        //[Route("movies/released/{year:regex(2015|2016}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

  /*      public ActionResult Index(int? pageIndex, string sortBy)
          {
              if (!pageIndex.HasValue)
                  pageIndex = 1;
              if (String.IsNullOrWhiteSpace(sortBy))
                  sortBy = "Name";

              return Content(String.Format("pageIndex={0}&sortBy={1} ",pageIndex,sortBy));

          }  */


        public ViewResult Index()
        {
            var movies = _context.Movies.Include(m => m.Genre).ToList();

            return View(movies);
        }
        public ActionResult Details(int id)
        {

            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id==id);
            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        private IEnumerable<Movie> GetMovies()
        {
            return new List<Movie>
            {
                new Movie { Id = 1, Name = "Shrek" },
                new Movie { Id = 2, Name = "Wall-e" }
            };
        }

    }
}