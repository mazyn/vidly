using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly.Database;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public MoviesController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public ActionResult<IEnumerable<MovieDto>> GetMovies()
        {
            var movies = _dbContext.Movies
                .Include(m => m.Genre)
                .ToList()
                .Select(_mapper.Map<MovieDto>);

            return Ok(movies);
        }

        [HttpGet("{id:int}")]
        public ActionResult<MovieDto> GetMovie(int id)
        {
            var movie = _dbContext.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == default)
                return NotFound();

            return Ok(_mapper.Map<MovieDto>(movie));
        }

        [HttpPost]
        public ActionResult<MovieDto> CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = _mapper.Map<Movie>(movieDto);

            _dbContext.Movies.Add(movie);
            _dbContext.SaveChanges();

            movieDto.Id = movie.Id;

            return CreatedAtAction(
                actionName: nameof(GetMovie),
                routeValues: new { id = movie.Id },
                value: movieDto);
        }

        [HttpPut("{id:int}")]
        public ActionResult<MovieDto> UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDb = _dbContext.Movies.SingleOrDefault(c => c.Id == id);

            if (movieInDb == default)
                return NotFound();

            _mapper.Map(movieDto, movieInDb);

            _dbContext.Movies.Update(movieInDb);
            _dbContext.SaveChanges();

            movieDto.Id = id;

            return Ok(movieDto);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteMovie(int id)
        {
            var movie = _dbContext.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == default)
                return NotFound();

            _dbContext.Movies.Remove(movie);
            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}