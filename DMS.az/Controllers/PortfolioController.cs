using DMS.az.DAL;
using DMS.az.Models;
using DMS.az.ViewModels.Portfolio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace DMS.az.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly AppDbContext _context;

        public PortfolioController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "Portfolio";

            var model = new PortfolioIndexVM
            {
                Portfolio = await _context.Portfolios.Where(p => !p.IsDeleted).Include(p => p.PortfolioCategory).ToListAsync(),
                PortfolioCategories = await _context.PortfolioCategories.Where(pc => !pc.IsDeleted).ToListAsync(),
            };
            return View(model);
        }

        public async Task<IActionResult> TabMenu(int id)
        {
            IQueryable<Portfolio> portfolios = _context.Portfolios.Where(p => !p.IsDeleted).Include(p => p.PortfolioCategory);

            if (id > 0)
            {
                portfolios = portfolios.Where(p => p.PortfolioCategoryId == id);
            }

            var model = new PortfolioTabMenuVM
            {
                Portfolios = await portfolios.ToListAsync()
            };

            return PartialView("_PortfolioTabMenuPartial", model.Portfolios);
        }
        public IActionResult Details(int id)
        {
            return View();
        }
    }
}
