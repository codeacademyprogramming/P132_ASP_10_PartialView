using System.ComponentModel.DataAnnotations;

namespace P132Pustok.ViewModels
{
    public class ReviewCreateViewModel
    {
        [Range(1,5)]
        public byte Rate { get; set; }
        [MaxLength(50)]
        public string Text { get; set; }
        public int BookId { get; set; }
    }
}
