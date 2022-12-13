using Microsoft.AspNetCore.Identity;

namespace P132Pustok.Models
{
    public class AppUser:IdentityUser
    {
        public string Fullname { get; set; }
    }
}
