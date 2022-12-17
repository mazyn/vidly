using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Dtos
{
    public record MovieDto
    {
        public int Id { get; set; }

        [Required] [StringLength(255)] public string Name { get; set; }

        public byte GenreId { get; set; }

        [Required] public DateTime ReleaseDate { get; set; }

        [Required] [Range(1, 20)] public byte? NumberInStock { get; set; }
    }
}