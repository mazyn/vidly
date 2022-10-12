using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public record Movie
    {
        public int Id { get; set; }
        [Required] [StringLength(255)] public string Name { get; set; }
        [Required] public MovieGenre Genre { get; set; }
        public byte GenreId { get; set; }
        [Required] public DateTime ReleaseDate { get; set; }
        [Required] public DateTime DateAdded { get; set; }
        [Required] public int NumberInStock { get; set; }
    }
}