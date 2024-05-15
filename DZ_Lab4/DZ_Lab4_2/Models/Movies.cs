using System;
using System.ComponentModel.DataAnnotations;

namespace DZ_Lab4_2.Models
{
    public class Movies
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }
        public float? Rate { get; set; }
    }
}
