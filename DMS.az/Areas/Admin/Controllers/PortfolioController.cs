using DMS.az.Areas.Admin.ViewModels.Portfolio;
using DMS.az.DAL;
using DMS.az.Models;
using DSM.az.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DSM.az.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PortfolioController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IFileService _fileService;

        public PortfolioController(AppDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        #region PortfolioList
        [HttpGet]
        public async Task<IActionResult> Index(PortfolioIndexVM model)
        {
            model = new PortfolioIndexVM
            {
                Portfolios = await _context.Portfolios.Include(p => p.PortfolioCategory).OrderByDescending(s => s.Id).Where(s => !s.IsDeleted).ToListAsync(),
            };

            return View(model);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {

            var model = new PortfolioCreateVM
            {
                PortfolioCategories = _context.PortfolioCategories.Where(pc => !pc.IsDeleted).Select(pc => new SelectListItem
                {
                    Text = pc.Name,
                    Value = pc.Id.ToString(),
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PortfolioCreateVM model)
        {
            var portfoliocategoryList = await _context.PortfolioCategories.Where(pc => !pc.IsDeleted).ToListAsync();

            model.PortfolioCategories = portfoliocategoryList.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).ToList();


            if (!ModelState.IsValid) return View(model);

            if (!_fileService.IsImage(model.Photo))
            {
                ModelState.AddModelError("MediaName", "Fayl formatı yalnışdır");
                return View(model);
            }

            if (!_fileService.IsBiggerThanSize(model.Photo, 2000))
            {
                ModelState.AddModelError("MediaName", "Faylın ölçüsü 2MB-dan böyükdür");
                return View(model);
            }

            var portfolio = new Portfolio
            {
                Title = model.Title,
                Description = model.Description,
                ShortDesc = model.ShortDesc,
                Photo = await _fileService.Upload(model.Photo),
                PortfolioCategoryId = model.PortfolioCategoryId,
                CreatedAt = DateTime.Now
            };

            _context.Portfolios.Add(portfolio);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Update(int id)
        {
            var portfolio = _context.Portfolios.FirstOrDefault(p => p.Id == id);
            if (portfolio is null) return NotFound("Portfolio Tapılmadı");

            var model = new PortfolioUpdateVM
            {
                PortfolioCategories = _context.PortfolioCategories.Where(pc => !pc.IsDeleted).Select(pc => new SelectListItem
                {
                    Text = pc.Name,
                    Value = pc.Id.ToString(),
                }).ToList(),


                Title = portfolio.Title,
                Description = portfolio.Description,
                ShortDesc = portfolio.ShortDesc,
                PhotoPath = portfolio.Photo,
                PortfolioCategoryId = portfolio.PortfolioCategoryId,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, PortfolioUpdateVM model)
        {
            var portfolio = _context.Portfolios.FirstOrDefault(s => s.Id == id);
            if (portfolio is null) return NotFound("Portfolio Tapılmadı!");

            model.PortfolioCategories = _context.PortfolioCategories.Where(pc => !pc.IsDeleted).Select(pc => new SelectListItem
            {
                Text = pc.Name,
                Value = pc.Id.ToString(),
            }).ToList();

            if (!ModelState.IsValid) return View();

            if (model.Photo is not null)
            {
                if (!_fileService.IsImage(model.Photo))
                {
                    ModelState.AddModelError("MediaName", "Fayl formatı yalnışdır");
                    return View();
                }

                if (!_fileService.IsBiggerThanSize(model.Photo, 2000))
                {
                    ModelState.AddModelError("MediaName", "Faylın ölçüsü 2MB-dan böyükdür");
                    return View();
                }
                _fileService.Delete(portfolio.Photo);
                portfolio.Photo = await _fileService.Upload(model.Photo);
            }

            portfolio.Title = model.Title;
            portfolio.Description = model.Description;
            portfolio.ShortDesc = model.ShortDesc;
            portfolio.PortfolioCategoryId = model.PortfolioCategoryId;
            portfolio.ModifiedAt = DateTime.Now;

            _context.Portfolios.Update(portfolio);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        #endregion

        public async Task<IActionResult> Details(int id)
        {
            var portfolio = await _context.Portfolios.FirstOrDefaultAsync(p => p.Id == id);
            if (portfolio == null) return NotFound("Portfolio Tapılmadı!");

            var selectedPortfolio = new PortfolioDetailsVM()
            {
                Title = portfolio.Title,
                Description = portfolio.Description,
                ShortDesc = portfolio.ShortDesc,
                PortfolioCategory = portfolio.PortfolioCategory,
                Photo = portfolio.Photo,
                CreatedAt = portfolio.CreatedAt,
                ModifiedAt = portfolio.ModifiedAt,
            };

            return  View(selectedPortfolio);
        }

        #region Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var portfolio = _context.Portfolios.FirstOrDefault(x => x.Id == id);
            if (portfolio is null) return NotFound("Portfolio Tapılmadı!");

            portfolio.IsDeleted = true;
            _context.Update(portfolio);
            _context.SaveChanges();

            return Ok();
        }
        #endregion
    }
}
