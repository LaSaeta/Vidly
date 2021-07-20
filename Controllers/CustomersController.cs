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
            var customers = _dataContext.Customers
                            .Include(c => c.MembershipType)
                            .ToList();

            var viewModel = new CustomerViewModel { Customers = customers };

            return View(viewModel);
        }


        public ActionResult Details(int id)
        {
            var customer = _dataContext.Customers
                            .Include(c => c.MembershipType)
                            .SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        public ActionResult Edit(int id)
        {
            var customer = _dataContext.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel()
            {
                Customer = customer,
                MembershipTypes = _dataContext.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }

        public ActionResult New()
        {
            var membershipTypes = _dataContext.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if (customer.Id == 0)
                _dataContext.Customers.Add(customer);
            else
            {
                var customerData = _dataContext.Customers.Single(c => c.Id == customer.Id);

                customerData.Name = customer.Name;
                customerData.BirthDate = customer.BirthDate;
                customerData.MembershipTypeId = customer.MembershipTypeId;
                customerData.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            }

            _dataContext.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}