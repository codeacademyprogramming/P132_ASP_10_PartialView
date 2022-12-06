using Microsoft.AspNetCore.Mvc;
using P132Pustok.DAL;
using P132Pustok.Helpers;
using P132Pustok.Models;

namespace P132Pustok.Areas.Manage.Controllers
{
    [Area("manage")]
    public class SliderController : Controller
    {
        private readonly PustokContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(PustokContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            var model = _context.Sliders.OrderBy(x=>x.Order).ToList();

            return View(model);
        }

        public IActionResult Create()
        {
            //int order = 1;

            //try
            //{
            //    order = _context.Sliders.Max(x => x.Order);
            //}
            //catch (Exception)
            //{
            //}

            var slider = _context.Sliders.OrderByDescending(x => x.Order).FirstOrDefault();
            int order = slider == null ? 1 : slider.Order+1;



            ViewBag.Order = order;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Slider slider)
        {
            if(slider.ImageFile!=null && slider.ImageFile.ContentType != "image/png" && slider.ImageFile.ContentType != "image/jpeg")
                ModelState.AddModelError("ImageFile", "Content type must be image/png or image/jpeg!");

            if (slider.ImageFile!=null && slider.ImageFile.Length > 2097152)
                ModelState.AddModelError("ImageFile", "File size must be less than 2MB!");

            if (!ModelState.IsValid)
            {
                return View();
            }

            slider.Image = FileManager.Save(slider.ImageFile, _env.WebRootPath, "uploads/sliders");


            _context.Sliders.Add(slider);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Delete(int id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(x => x.Id == id);

            if (slider == null)
                return NotFound();

            FileManager.Delete(_env.WebRootPath,"uploads/sliders", slider.Image);

            _context.Sliders.Remove(slider);
            _context.SaveChanges();

            return Ok();
        }
    }
}
