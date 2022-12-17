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
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet("/[controller]")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("{id:int:min(1)}")]
        public IActionResult Details(int id)
        {
            var movie = _context.Movies
                .Include(m => m.Genre)
                .SingleOrDefault(m => m.Id == id);

            return View(movie);
        }

        public IActionResult New()
        {
            var movieGenres = _context.MovieGenre.ToList();
            var viewModel = new MovieFormViewModel(movieGenres);

            return View("MovieForm", viewModel);
        }

        [Route("{id:int:min(1)}")]
        public IActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == default)
                return NotFound();

            var movieGenres = _context.MovieGenre.ToList();
            var viewModel = new MovieFormViewModel(movie, movieGenres);

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var movieGenres = _context.MovieGenre.ToList();
                var viewModel = new MovieFormViewModel(movie, movieGenres);

                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
                _context.Movies.Add(movie);
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
                _context.Movies.Update(movieInDb);
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }
    }
}