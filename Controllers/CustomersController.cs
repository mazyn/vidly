using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    [Route("[controller]")]
    public class CustomersController : Controller
    {
        private List<Customer> _customers = new()
        {
            new Customer { Id = 1, Name = "Foo" },
            new Customer { Id = 2, Name = "Bar" },
        };

        // GET
        public IActionResult Index()
        {
            var viewModel = new CustomersIndexViewModel
            {
                Customers = _customers
            };

            return View(viewModel);
        }

        [Route("Details/{id:int:min(1)}")]
        public IActionResult Details(int id)
        {
            var customer = _customers.Find(c => c.Id.Equals(id));

            return View(customer);
        }
    }
}