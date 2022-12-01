using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P132Pustok.DAL;
using P132Pustok.ViewModels;
using System.Diagnostics;

namespace P132Pustok.Controllers
{
    public class HomeController : Controller
    {
        private readonly PustokContext _context;

        public HomeController(PustokContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //var data = _context.Books.ToList();
            //var book = _context.Books.FirstOrDefault(x => x.Id == 5);
            //book = _context.Books.First(x => x.Id == 5);
            //book = _context.Books.SingleOrDefault(x => x.Id == 5);


            //var query = _context.Books.Where(x => x.CostPrice > 10);
            //query = query.OrderBy(x => x.CostPrice);
            //query = query.Where(x => x.AuthorId > 1);

            //data = _context.Books.FromSqlRaw("select * from books where salePrice>10").ToList();



            HomeViewModel homeVM = new HomeViewModel
            {
                SpecialBooks = _context.Books.Include(x => x.Author).Include(x => x.BookImages).Where(x => x.IsSpecial).Take(20).ToList(),
                NewBooks = _context.Books.Include(x => x.Author).Include(x => x.BookImages).Where(x => x.IsNew).Take(20).ToList(),
                DiscountedBooks = _context.Books.Include(x => x.Author).Include(x => x.BookImages).Where(x => x.DiscountPercent > 0).Take(20).ToList(),
                Sliders = _context.Sliders.OrderBy(x => x.Order).Take(10).ToList(),
                Settings = _context.Settings.ToDictionary(x => x.Key, x => x.Value)
            };
            return View(homeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}