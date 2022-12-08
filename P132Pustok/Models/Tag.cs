using System.ComponentModel.DataAnnotations;

namespace P132Pustok.Models
{
    public class Tag
    {
        public int Id { get; set; }
        [MaxLength(25)]
        public string Name { get; set; }

        public List<BookTag> BookTags { get; set; } 
    }
}
