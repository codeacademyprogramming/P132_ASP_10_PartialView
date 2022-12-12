using Microsoft.AspNetCore.Mvc;
using P132Pustok.DAL;
using P132Pustok.Models;
using System.Linq;

namespace P132Pustok.Areas.Manage.Controllers
{
    [Area("manage")]
    public class AccountController : Controller
    {
        private readonly PustokContext _context;

        public AccountController(PustokContext context)
        {
            _context = context;
        }
        public IActionResult SetCookie(string name)
        {
            HttpContext.Response.Cookies.Append("username", name);
            return Ok();
        }

        public IActionResult GetCookie()
        {
            var name = HttpContext.Request.Cookies["username"];
            return Content("Cookie - " + name);
        }

        public IActionResult SetSession(string name)
        {
            HttpContext.Session.SetString("username", name);
            return Ok();
        }

        public IActionResult GetSession()
        {
            var name = HttpContext.Session.GetString("username");
            return Content("Session - " + name);
        }

        public IActionResult DeleteCookie()
        {
            HttpContext.Response.Cookies.Delete("username");
            return Ok();
        }

        public IActionResult DeleteSession()
        {
            HttpContext.Session.Remove("username");
            return Ok();
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username,string password)
        {
            Account account = _context.Accounts.FirstOrDefault(x => x.Username == username);

            if(account == null)
            {
                return RedirectToAction("login");
            }

            if (!BCrypt.Net.BCrypt.Verify(password, account.PassHash))
            {
                return RedirectToAction("login");
            }

            HttpContext.Session.SetString("username", account.Username);

            return RedirectToAction("index", "dashboard");
        }

        public IActionResult Register()
        {
            Account account = new Account
            {
                Username = "seymur",
                PassHash = BCrypt.Net.BCrypt.HashPassword("Admin123"),
                Role = "Admin"
            };

            _context.Accounts.Add(account);
            _context.SaveChanges();

            return Ok();
        }
    }
}
