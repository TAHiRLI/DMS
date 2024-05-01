using DMS.az.Areas.Admin.ViewModels.Blogs;
using DMS.az.Areas.Admin.ViewModels.CompanyHistory;
using DMS.az.DAL;
using DMS.az.Models;
using DSM.az.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DMS.az.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyHistoriesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IFileService _fileService;

        public CompanyHistoriesController(AppDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        #region CompanyHistoryList
        [HttpGet]
        public async Task<IActionResult> Index(HistoryIndexVM model)
        {
            model = new HistoryIndexVM
            {
                CompanyHistories = await _context.CompanyHistories.OrderByDescending(b => b.Id).Where(b => !b.IsDeleted).ToListAsync()
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
        public async Task<IActionResult> Create(HistoryCreateVM model)
        {
            if (!ModelState.IsValid) return View();

            var companyHistory = new CompanyHistory
            {
                Title = model.Title,
                Description = model.Description,
                EventDate = model.EventDate,
                EventDateAsString = model.EventDateAsString,
                CreatedAt = DateTime.Now
            };

            _context.CompanyHistories.Add(companyHistory);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Update(int id)
        {
            var companyHistory = _context.CompanyHistories.FirstOrDefault(b => b.Id == id);
            if (companyHistory is null) return NotFound("Şirkət Tarixi Tapılmadı!");

            var model = new HistoryUpdateVM
            {
                Title = companyHistory.Title,
                Description = companyHistory.Description,
                EventDate = companyHistory.EventDate,
                EventDateAsString = companyHistory.EventDateAsString,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, HistoryUpdateVM model)
        {
            var companyHistory = _context.CompanyHistories.FirstOrDefault(e => e.Id == id);
            if (companyHistory is null) return NotFound("Şirkət Tarixi Tapılmadı!");


            if (!ModelState.IsValid) return View();

            companyHistory.Title = model.Title;
            companyHistory.Description = model.Description;
            companyHistory.EventDate = model.EventDate;
            companyHistory.EventDateAsString = model.EventDateAsString;
            companyHistory.ModifiedAt = DateTime.Now;

            _context.CompanyHistories.Update(companyHistory);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var companyHistory = _context.CompanyHistories.FirstOrDefault(x => x.Id == id);
            if (companyHistory is null) return NotFound("Compant History Not Found!");

            companyHistory.IsDeleted = true;
            _context.Update(companyHistory);
            _context.SaveChanges();

            return Ok();
        }
        #endregion

        #region Details
        public async Task<IActionResult> Details(int id)
        {
            var companyHistory = await _context.CompanyHistories.FirstOrDefaultAsync(p => p.Id == id);
            if (companyHistory == null) return NotFound("Şirkət Tarixi Tapılmadı!");

            var selectedCompanyHistory = new HistoryDetailsVM()
            {
                Title = companyHistory.Title,
                Description = companyHistory.Description,
                EventDate = companyHistory.EventDate,
                EventDateAsString = companyHistory.EventDateAsString,
                CreatedAt = companyHistory.CreatedAt,
                ModifiedAt = companyHistory.ModifiedAt,
            };

            return View(selectedCompanyHistory);
        }
        #endregion
    }
}
