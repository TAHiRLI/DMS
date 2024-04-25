using DMS.az.Areas.Admin.ViewModels.AboutUs;
using DMS.az.DAL;
using DMS.az.Models;
using DSM.az.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DMS.az.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutUsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IFileService _fileService;

        public AboutUsController(AppDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        #region AboutUsList
        [HttpGet]
        public async Task<IActionResult> Index(AboutUsIndexVM model)
        {
            model = new AboutUsIndexVM
            {
                AboutUs = await _context.AboutUs.OrderByDescending(s => s.Id).Where(s => !s.IsDeleted).ToListAsync(),
            };

            return View(model);
        }
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Update(int id)
        {
            var aboutUs = _context.AboutUs.FirstOrDefault(p => p.Id == id);
            if (aboutUs is null) return NotFound();

            var model = new AboutUsUpdateVM
            {
                PhotoName1 = aboutUs.Photo1,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, AboutUsUpdateVM model)
        {
            var aboutUs = _context.AboutUs.FirstOrDefault(s => s.Id == id);
            if (aboutUs is null) return NotFound();

            if (!ModelState.IsValid) return View();

            if (model.Photo1 is not null)
            {
                if (!_fileService.IsImage(model.Photo1))
                {
                    ModelState.AddModelError("Photo1", "Fayl formatı yalnışdır");
                    return View();
                }

                if (!_fileService.IsBiggerThanSize(model.Photo1, 2000))
                {
                    ModelState.AddModelError("Photo1", "Faylın ölçüsü 2MB-dan böyükdür");
                    return View();
                }
                _fileService.Delete(aboutUs.Photo1);
                aboutUs.Photo1 =await _fileService.Upload(model.Photo1, "Users/images");
            }

            _context.AboutUs.Update(aboutUs);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
