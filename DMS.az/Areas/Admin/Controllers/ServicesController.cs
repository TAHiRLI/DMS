using DMS.az.Areas.Admin.ViewModels.Services;
using DMS.az.DAL;
using DMS.az.Models;
using DSM.az.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace DMS.az.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServicesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IFileService _fileService;

        public ServicesController(AppDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        #region ServicesList
        [HttpGet]
        public async Task<IActionResult> Index(ServiceIndexVM model)
        {
            model = new ServiceIndexVM
            {
                Services = await _context.Services.OrderByDescending(s => s.Id).Where(s => !s.IsDeleted).ToListAsync(),
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
        public async Task<IActionResult> Create(ServiceCreateVM model)
        {
            if (!ModelState.IsValid) return View();

            if (!_fileService.IsImage(model.Icon))
            {
                ModelState.AddModelError("Icon", "Fayl formatı yalnışdır");
                return View();
            }

            if (!_fileService.IsBiggerThanSize(model.Icon, 2000))
            {
                ModelState.AddModelError("Icon", "Faylın ölçüsü 2MB-dan böyükdür");
                return View();
            }

            var serviceByName = await _context.Services.FirstOrDefaultAsync(s => s.Name.Trim().ToLower() == model.Name.Trim().ToLower());

            if (serviceByName is not null)
            {
                ModelState.AddModelError("Name", "Bu adda xidmət mövcuddur");
                return View(model);
            }

            var service = new Service
            {
                Name = model.Name,
                Description = model.Description,
                ShortDesc = model.ShortDesc,
                ServiceQualification = model.ServiceQualification,
                Icon= await _fileService.Upload(model.Icon, "Users/Uploads/Services"),
                Photo = await _fileService.Upload(model.Photo, "Users/Uploads/Services"),
                CreatedAt = DateTime.Now
            };

            _context.Services.Add(service);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Update(int id)
        {
            var service = _context.Services.FirstOrDefault(p => p.Id == id);
            if (service is null) return NotFound("Xidmət Tapılmadı");

            var model = new ServiceUpdateVM
            {
                Name = service.Name,
                Description = service.Description,
                ShortDesc = service.ShortDesc,
                ServiceQualification = service.ServiceQualification,
                PhotoName = service.Photo,
                İconPath = service.Icon
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, ServiceUpdateVM model)
        {
            var service = _context.Services.FirstOrDefault(s => s.Id == id);
            if (service is null) return NotFound("Xidmət Tapılmadı");


            if (!ModelState.IsValid) return View();

            if (model.Icon is not null)
            {
                if (!_fileService.IsImage(model.Icon))
                {
                    ModelState.AddModelError("Icon", "Fayl formatı yalnışdır");
                    return View();
                }

                if (!_fileService.IsBiggerThanSize(model.Icon, 2000))
                {
                    ModelState.AddModelError("Icon", "Faylın ölçüsü 2MB-dan böyükdür");
                    return View();
                }
                _fileService.Delete(service.Icon);
                service.Icon = await _fileService.Upload(model.Icon, "Users/Uploads/Services");
            }
            if (model.Photo is not null)
            {
                _fileService.Delete(service.Photo);
                service.Photo = await _fileService.Upload(model.Photo, "Users/Uploads/Services");
            }
            service.Name = model.Name;
            service.Description = model.Description;
            service.ShortDesc = model.ShortDesc;
            service.ServiceQualification = model.ServiceQualification;
            service.ModifiedAt = DateTime.Now;

            _context.Services.Update(service);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var service = _context.Services.FirstOrDefault(x => x.Id == id);
            if (service is null) return NotFound("Xidmət Tapılmadı!");

            service.IsDeleted = true;
            _context.Update(service);
            _context.SaveChanges();

            return Ok();
        }
        #endregion

        #region Details
        public async Task<IActionResult> Details(int id)
        {
            var service = await _context.Services.FirstOrDefaultAsync(x => x.Id == id);
            if (service is null) return NotFound("Xidmət Tapılmadı!");

            var selectedService = new ServiceDetailsVM()
            {
                Name = service.Name,
                Description = service.Description,
                ShortDesc = service.ShortDesc,
                Icon = service.Icon,
                Photo = service.Photo,
                ServiceQualification = service.ServiceQualification,
                CreatedAt = service.CreatedAt,
                ModifiedAt = service.ModifiedAt,
            };

            return View(selectedService);
        }

        #endregion
    }
}
