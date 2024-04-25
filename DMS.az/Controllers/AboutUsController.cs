using DMS.az.DAL;
using DMS.az.ViewModels.AboutUs;
using DMS.az.ViewModels.Home;
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
                TeamMembers = await _context.TeamMembers.ToListAsync(),
            };

            return View(model);
        }

        public async Task<IActionResult> LoadMore(int skipRow)
        {

            var model = new AboutUsLoadMore
            {
                TeamMembers = await _context.TeamMembers.OrderByDescending(x => x.Id).Skip(4 * skipRow).Take(4).ToListAsync()
            };

            return PartialView("_ServicesComponentPartial", model);
        }

    }
}
