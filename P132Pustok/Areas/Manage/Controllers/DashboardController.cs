using Microsoft.AspNetCore.Mvc;
using P132Pustok.DAL;
using P132Pustok.Models;

namespace P132Pustok.Areas.Manage.Controllers
{
    [Area("manage")]
    public class DashboardController : Controller
    {
        private readonly PustokContext _context;

        public DashboardController(PustokContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            string username = HttpContext.Session.GetString("username");

            Account account = _context.Accounts.FirstOrDefault(x => x.Username == username);

            if(account == null)
            {
                RedirectToAction("login", "account");
            }

            ViewBag.LoggedUser = account.Username;

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
