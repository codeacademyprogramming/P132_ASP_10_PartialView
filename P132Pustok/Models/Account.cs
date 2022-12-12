using System.ComponentModel.DataAnnotations;

namespace P132Pustok.Models
{
    public class Account
    {
        public int Id { get; set; }
        [MaxLength(25)]
        public string Username { get; set; }
        public string PassHash { get; set; }
        public string Role { get; set; }
    }
}
