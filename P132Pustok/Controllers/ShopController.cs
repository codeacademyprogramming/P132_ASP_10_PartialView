using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P132Pustok.DAL;
using P132Pustok.Models;
using P132Pustok.ViewModels;

namespace P132Pustok.Controllers
{
    public class ShopController : Controller
    {
        private readonly PustokContext _context;

        public ShopController(PustokContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page=1,int? genreId=null,List<int> authorIds=null,List<int> tagIds = null,decimal? minPrice=null,decimal? maxPrice=null,string sort="AtoZ")
        {
            ViewBag.SelectedGenreId = genreId;
            ViewBag.SelectedAuthorIds = authorIds;

          
            var books = _context.Books.Include(x => x.Author).Include(x => x.BookImages).AsQueryable();
            
            if(genreId != null)
                books = books.Where(x => x.GenreId == genreId);

            if (authorIds != null && authorIds.Count>0)
                books = books.Where(x => authorIds.Contains(x.AuthorId));

         

            if (minPrice!=null && maxPrice != null)
            {
                books = books.Where(x => x.SalePrice >= minPrice && x.SalePrice <= maxPrice);
               
            }

            switch (sort)
            {
                case "ZToA":
                    books = books.OrderByDescending(x => x.Name);
                    break;
                case "LowToHigh":
                    books = books.OrderBy(x => x.SalePrice);
                    break;
                case "HighToLow":
                    books = books.OrderByDescending(x => x.SalePrice);
                    break;
                default:
                    books = books.OrderBy(x => x.Name);
                    break;
            }




            ShopViewModel model = new ShopViewModel
            {
                Books = PaginatedList<Book>.Create(books, page, 4),
                Authors = _context.Authors.Include(x => x.Books).Where(x => x.Books.Count > 0).ToList(),
                Genres = _context.Genres.Include(x => x.Books).Where(x=>x.Books.Count>0).ToList(),
                Tags = _context.Tags.ToList(),
                MinPrice = _context.Books.Min(x=>x.SalePrice),
                MaxPrice = _context.Books.Max(x => x.SalePrice),
            };

            ViewBag.SelectedMinPrice = minPrice ?? model.MinPrice;
            ViewBag.SelectedMaxPrice = maxPrice ?? model.MaxPrice;

            return View(model);
        }
    }
}
