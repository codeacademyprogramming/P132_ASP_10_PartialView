using System.ComponentModel.DataAnnotations;

namespace P132Pustok.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [MaxLength(100)]
        public string Email { get; set; }
    }
}
