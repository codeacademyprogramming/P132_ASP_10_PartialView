using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P132Pustok.DAL;
using P132Pustok.Models;
using P132Pustok.ViewModels;

namespace P132Pustok.Controllers
{
    public class BookController : Controller
    {
        private readonly PustokContext _context;

        public BookController(PustokContext context)
        {
            _context = context;
        }
        
        public IActionResult GetBook(int id)
        {
            Book book = _context.Books.Include(x=>x.Genre).Include(x=>x.BookImages).FirstOrDefault(x => x.Id == id);

            //return Ok(book);
            return PartialView("_BookModalPartial", book);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Book book = _context.Books
                .Include(x=>x.Genre)
                .Include(x=>x.Author)
                .Include(x=>x.BookImages)
                .Include(x=>x.BookTags).ThenInclude(x=>x.Tag)
                .FirstOrDefault(x => x.Id == id);

            BookDetailViewModel detailVM = new BookDetailViewModel
            {
                Book = book,
                ReviewVM = new ReviewCreateViewModel { BookId = id},
                RelatedBooks = _context.Books.Where(x => x.GenreId == book.GenreId || x.AuthorId == book.AuthorId).Take(8).ToList()
            };

            if (book == null)
                return NotFound();

            return View(detailVM);
        }

        [HttpPost]
        public IActionResult Review(ReviewCreateViewModel review)
        {

            if (!ModelState.IsValid)
            {
                Book book = _context.Books
               .Include(x => x.Genre)
               .Include(x => x.Author)
               .Include(x => x.BookImages)
               .Include(x => x.BookTags).ThenInclude(x => x.Tag)
               .FirstOrDefault(x => x.Id == review.BookId);

                BookDetailViewModel detailVM = new BookDetailViewModel
                {
                    Book = book,
                    RelatedBooks = _context.Books.Where(x => x.GenreId == book.GenreId || x.AuthorId == book.AuthorId).Take(8).ToList(),
                    ReviewVM = review
                };

                return View("detail",detailVM);
            }

            return Ok(review);
        }

    }
}
