using P132Pustok.Models;

namespace P132Pustok.ViewModels
{
    public class BasketItemViewModel
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public Book Book { get; set; }
    }
}
