using System.Collections.Generic;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public record MovieFormViewModel
    {
        public Movie Movie { get; set; }
        public IEnumerable<MovieGenre> MovieGenres { get; set; }

        public MovieFormViewModel(Movie movie, IEnumerable<MovieGenre> movieGenres)
        {
            Movie = movie;
            MovieGenres = movieGenres;
        }
    }
}