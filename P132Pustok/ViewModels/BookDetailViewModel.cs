using P132Pustok.Models;

namespace P132Pustok.ViewModels
{
    public class BookDetailViewModel
    {
        public Book Book { get; set; }
        public List<Book> RelatedBooks { get; set; }
        public ReviewCreateViewModel ReviewVM { get; set; }
        public bool HasReview { get; set; }
    }
}
