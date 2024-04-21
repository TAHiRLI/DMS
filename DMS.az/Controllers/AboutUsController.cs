using DMS.az.DAL;
using DMS.az.ViewModels.AboutUs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DMS.az.Controllers
{
    public class AboutUsController : Controller
    {
        private readonly AppDbContext _context;

        public AboutUsController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "Haqqımızda";


            var model = new AboutUsIndexVM
            {
                AboutUs = await _context.AboutUs.ToListAsync(),
            };

            return View(model);
        }
    }
}
