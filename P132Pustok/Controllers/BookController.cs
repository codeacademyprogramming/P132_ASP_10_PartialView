using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P132Pustok.DAL;
using P132Pustok.Models;

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

    }
}
