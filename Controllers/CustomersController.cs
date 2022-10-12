using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Vidly.Database;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    [Route("[controller]")]
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
        public IActionResult Index()
        {
            var customers = _context.Customers.ToList();
            
            var viewModel = new CustomersIndexViewModel
            {
                Customers = customers
            };

            return View(viewModel);
        }

        [Route("Details/{id:int:min(1)}")]
        public IActionResult Details(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            return View(customer);
        }
    }
}