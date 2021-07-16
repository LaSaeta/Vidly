using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Data;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private DataContext _dataContext { get; set; }

        public CustomersController()
        {
            _dataContext = new DataContext();
        }

        // GET: Customers

        public ActionResult Index()
        {
            var customers = _dataContext.Customers.Include(c => c.MembershipType).ToList();

            var viewModel = new CustomerViewModel { Customers = customers };

            return View(viewModel);
        }


        public ActionResult Details(int id)
        {
            var customer = _dataContext.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

    }
}