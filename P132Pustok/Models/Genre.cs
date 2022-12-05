using System.ComponentModel.DataAnnotations;

namespace P132Pustok.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Mecburidir, qaqa!")]
        [MaxLength(50)]
        public string Name { get; set; }

        public List<Book>? Books { get; set; }
    }
}
