using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P132Pustok.Areas.Manage.ViewModels;
using P132Pustok.DAL;
using P132Pustok.Models;

namespace P132Pustok.Areas.Manage.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin,Editor")]
    [Area("manage")]
    public class OrderController : Controller
    {
        private readonly PustokContext _context;

        public OrderController(PustokContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page=1)
        {
            var query = _context.Orders
              .Include(x => x.OrderItems);


            var model = PaginatedList<Order>.Create(query, page, 4);
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            Order order = _context.Orders.Include(x=>x.OrderItems).FirstOrDefault(x => x.Id == id);

            if (order == null)
                return RedirectToAction("error", "dashboard");



            return View(order);
        }

        [HttpPost]
        public IActionResult Accept(int id)
        {
            Order order = _context.Orders.FirstOrDefault(x => x.Id == id);

            if(order==null)
                return RedirectToAction("error", "dashboard");

            order.Status = Enums.OrderStatus.Accepted;

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        [HttpPost]
        public IActionResult Reject(int id)
        {
            Order order = _context.Orders.FirstOrDefault(x => x.Id == id);

            if (order == null)
                return RedirectToAction("error", "dashboard");

            order.Status = Enums.OrderStatus.Rejected;

            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
