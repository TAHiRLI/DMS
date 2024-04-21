using DMS.az.Areas.Admin.ViewModels.Portfolio;
using DMS.az.Areas.Admin.ViewModels.PortfolioCategory;
using DMS.az.DAL;
using DMS.az.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DMS.az.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PortfolioCategoryController : Controller
    {
        private readonly AppDbContext _context;

        public PortfolioCategoryController(AppDbContext context)
        {
            _context = context;
        }


        #region PortfolioCategoryList
        [HttpGet]
        public async Task<IActionResult> Index(PortfolioCategoryIndexVM model)
        {
            model = new PortfolioCategoryIndexVM()
            {
                PortfolioCategories = await _context.PortfolioCategories.OrderByDescending(s => s.Id).Where(s => !s.IsDeleted).ToListAsync(),
            };

            return View(model);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PortfolioCategoryCreateVM model)
        {
            if (!ModelState.IsValid) return View();

            var portfolioCategoryByName = await _context.PortfolioCategories.Where(pc => !pc.IsDeleted).FirstOrDefaultAsync(pc => pc.Name == model.Name);

            if (portfolioCategoryByName is not null)
            {
                ModelState.AddModelError("Name", "Bu adda kateqoriya mövcuddur");
                return View();
            }

            var portfolioCategory = new PortfolioCategory
            {
                Name = model.Name,
                CreatedAt = DateTime.Now
            };

            _context.PortfolioCategories.Add(portfolioCategory);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Update(int id)
        {
            var portfolioCategory = _context.PortfolioCategories.FirstOrDefault(pc => pc.Id == id);
            if (portfolioCategory is null) return NotFound("Kateqoriya Tapılmadı");

            var model = new PortfolioCategoryUpdateVM
            {
                Name = portfolioCategory.Name,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, PortfolioCategoryUpdateVM model)
        {
            var portfolioCategory =  await _context.PortfolioCategories.FirstOrDefaultAsync(pc => pc.Id == id);
            if (portfolioCategory is null) return NotFound("Kateqoriya Tapılmadı!");


            if (!ModelState.IsValid) return View();

            var portfolioCategoryByName =  await _context.PortfolioCategories.Where(pc => !pc.IsDeleted).FirstOrDefaultAsync(pc => pc.Name == model.Name);

            if (portfolioCategoryByName is not null)
            {
                ModelState.AddModelError("Name", "Bu adda kateqoriya mövcuddur");
                return View();
            }

            portfolioCategory.Name = model.Name;
            portfolioCategory.ModifiedAt = DateTime.Now;

            _context.PortfolioCategories.Update(portfolioCategory);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var portfolioCategory = _context.PortfolioCategories.FirstOrDefault(x => x.Id == id);
            if (portfolioCategory is null) return NotFound("Kateqoriya Tapılmadı!");

            portfolioCategory.IsDeleted = true;
            _context.Update(portfolioCategory);
            _context.SaveChanges();

            return Ok();
        }
        #endregion
    }
}
