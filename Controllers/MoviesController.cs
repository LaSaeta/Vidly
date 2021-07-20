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
        private DataContext _dataContext { get; set; }
        public MoviesController()
        {
            _dataContext = new DataContext();
        }

        // GET: Movies
        public ActionResult Index()
        {
            var movies = _dataContext.Movies
                         .Include(m => m.Genre)
                         .ToList();


            var viewModel = new MovieViewModel { Movies = movies };

            return View(viewModel);
        }

        public ActionResult Details(int id)
        {
            var movie = _dataContext.Movies
                        .Include(m => m.Genre)
                        .SingleOrDefault(m => m.Id == id);

            return View(movie);
        }

        public ActionResult Edit(int id)
        {
            var movie = _dataContext.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel()
            {
                Movie = movie,
                Genres = _dataContext.Genres.ToList()

            };

            return View("MovieForm", viewModel);
        }

        public ActionResult New()
        {
            var genres = _dataContext.Genres.ToList();
            var viewModel = new MovieFormViewModel()
            {
                Genres = genres
            };

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            if (movie.Id == 0)
                _dataContext.Movies.Add(movie);
            else
            {
                var movieData = _dataContext.Movies.Single(m => m.Id == movie.Id);

                movieData.Name = movie.Name;
                movieData.ReleaseDate = movie.ReleaseDate;
                movieData.GenreId = movie.GenreId;
                movieData.NumberInStock = movie.NumberInStock;
            }

            _dataContext.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }
    }
}