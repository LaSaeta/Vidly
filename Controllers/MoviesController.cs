using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Data;
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
    }
}