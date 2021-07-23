using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Data;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private readonly DataContext _context;
        public MoviesController()
        {
            _context = new DataContext();
        }

        // GET: Movies
        public ActionResult Index()
        {
            var movies = _context.Movies
                         .Include(m => m.Genre)
                         .ToList();


            var viewModel = new MovieViewModel { Movies = movies };

            return View(viewModel);
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies
                        .Include(m => m.Genre)
                        .SingleOrDefault(m => m.Id == id);

            return View(movie);
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = _context.Genres.ToList()

            };

            return View("MovieForm", viewModel);
        }

        public ActionResult New()
        {
            var genres = _context.Genres.ToList();
            var viewModel = new MovieFormViewModel()
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

            if (movie.Id == 0)
                _context.Movies.Add(movie);
            else
            {
                var movieData = _context.Movies.Single(m => m.Id == movie.Id);

                movieData.Name = movie.Name;
                movieData.ReleaseDate = movie.ReleaseDate;
                movieData.GenreId = movie.GenreId;
                movieData.NumberInStock = movie.NumberInStock;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }
    }
}