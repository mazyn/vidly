using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public record Movie
    {
        public int Id { get; set; }
        [Required] [StringLength(255)] public string Name { get; set; }
        public MovieGenre Genre { get; set; }
        [Required] public byte GenreId { get; set; }
        [Required] public DateTime ReleaseDate { get; set; }
        [Required] public DateTime DateAdded { get; set; }

        [Required]
        [Range(1, 20)]
        [DisplayName("Number in stock")]
        public byte? NumberInStock { get; set; }
    }
}