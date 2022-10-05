using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private List<Movie> _movies = new()
        {
            new Movie() { Name = "Shrek" },
            new Movie() { Name = "Wall-e" }
        };

        public IActionResult Index()
        {
            return View(_movies);
        }

        // GET Movies/Random
        public IActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };
            var customers = new List<Customer>
            {
                new() { Name = "Customer 1" },
                new() { Name = "Customer 2" }
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
        }
    }
}