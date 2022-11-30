using System.ComponentModel.DataAnnotations;

namespace P132Pustok.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
