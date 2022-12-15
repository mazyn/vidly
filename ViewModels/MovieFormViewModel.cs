using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public record MovieFormViewModel
    {
        public int? Id { get; set; }
        [Required] [StringLength(255)] public string Name { get; set; }
        [Required] [DisplayName("Genre")] public byte? GenreId { get; set; }
        [Required] public DateTime? ReleaseDate { get; set; }

        [Required]
        [Range(1, 20)]
        [DisplayName("Number in stock")]
        public byte? NumberInStock { get; set; }

        public IEnumerable<MovieGenre> MovieGenres { get; set; }

        public MovieFormViewModel(IEnumerable<MovieGenre> movieGenres)
        {
            Id = 0;
            MovieGenres = movieGenres;
        }

        public MovieFormViewModel(Movie movie, IEnumerable<MovieGenre> movieGenres)
        {
            Id = movie.Id;
            Name = movie.Name;
            GenreId = movie.GenreId;
            ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;
            MovieGenres = movieGenres;
        }
    }
}