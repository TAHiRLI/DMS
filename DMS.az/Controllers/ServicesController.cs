using DMS.az.DAL;
using DMS.az.ViewModels.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DMS.az.Controllers
{
	public class ServicesController : Controller
	{
		private readonly AppDbContext _context;

		public ServicesController(AppDbContext context)
        {
			_context = context;
		}
        public async Task<IActionResult> Index()
		{
            ViewBag.Title = "Xidmətlər";

            var model = new ServicesIndexVM
			{
				Services = await _context.Services.Where(s => !s.IsDeleted).OrderByDescending(x => x.Id).ToListAsync(),
			};

			return View(model);
		}
		public IActionResult Details(int id)
		{
			return View();
		}
	}
}
