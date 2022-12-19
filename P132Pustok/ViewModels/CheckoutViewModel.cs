using P132Pustok.Models;

namespace P132Pustok.ViewModels
{
    public class CheckoutViewModel
    {
        public Order Order { get; set; }
        public List<CheckoutItemViewModel> CheckoutItems { get; set; } = new List<CheckoutItemViewModel>();
        public decimal Total { get; set; }
    }
}
