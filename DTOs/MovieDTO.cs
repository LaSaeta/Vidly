using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.DTOs
{
    public class MovieDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.Now;

        [Required]
        [Range(1, 20)]
        public int NumberInStock { get; set; }

        [Required]
        public int GenreId { get; set; }
    }
}