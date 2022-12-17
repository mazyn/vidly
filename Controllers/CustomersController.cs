using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly.Database;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    [Controller]
    [Route("[controller]/[action]")]
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET
        [HttpGet("/[controller]")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("{id:int:min(1)}")]
        public IActionResult Details(int id)
        {
            var customer = _context.Customers
                .Include(c => c.MembershipType)
                .SingleOrDefault(c => c.Id == id);

            return View(customer);
        }

        public IActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm", viewModel);
        }

        [Route("{id:int:min(1)}")]
        public IActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == default)
                return NotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };

                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                _context.Customers.Update(customerInDb);
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }
    }
}