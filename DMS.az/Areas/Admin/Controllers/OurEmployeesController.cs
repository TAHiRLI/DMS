using DMS.az.Areas.Admin.ViewModels.OurEmployees;
using DMS.az.DAL;
using DMS.az.Models;
using DSM.az.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace DMS.az.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin, SuperAdmin")]
    public class OurEmployeesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IFileService _fileService;

        public OurEmployeesController(AppDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        #region OurEmployeesList
        [HttpGet]
        public async Task<IActionResult> Index(OurEmployeesIndexVM model)
        {
            model = new OurEmployeesIndexVM
            {
                Clients = await _context.OurEmployees.OrderByDescending(s => s.Id).Where(s => !s.IsDeleted).ToListAsync(),
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
        public async Task<IActionResult> Create(OurEmployeesCreateVM model)
        {
            if (!ModelState.IsValid) return View();

            if (!_fileService.IsImage(model.Photo))
            {
                ModelState.AddModelError("Photo", "Fayl formatı yalnışdır");
                return View();
            }

            if (!_fileService.IsBiggerThanSize(model.Photo, 2000))
            {
                ModelState.AddModelError("Photo", "Faylın ölçüsü 2MB-dan böyükdür");
                return View();
            }

            var employee = new Client()
            {
                Photo =await _fileService.Upload(model.Photo, "Users/Uploads/OurCustomers"),
                RedirectLink = model.RedirectLink,
            };
            _context.OurEmployees.Add(employee);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Update(int id)
        {
            var employee = _context.OurEmployees.FirstOrDefault(e => e.Id == id);
            if (employee is null) return NotFound("Əməkdaş Tapılmadı!");

            var model = new OurEmployeesUpdateVM
            {
                RedirectLink = employee.RedirectLink,
                PhotoName = employee.Photo
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, OurEmployeesUpdateVM model)
        {
            var employee = _context.OurEmployees.FirstOrDefault(e => e.Id == id);
            if (employee is null) return NotFound("Əməkdaş Tapılmadı!");

////            if (!ModelState.IsValid) return View();

            if (!ModelState.IsValid) return View();

            if (model.Photo is not null)
            {
                if (!_fileService.IsImage(model.Photo))
                {
                    ModelState.AddModelError("Photo", "Fayl formatı yalnışdır");
                    return View();
                }

                if (!_fileService.IsBiggerThanSize(model.Photo, 2000))
                {
                    ModelState.AddModelError("Photo", "Faylın ölçüsü 2MB-dan böyükdür");
                    return View();
                }
                _fileService.Delete(employee.Photo);
                employee.Photo = await _fileService.Upload(model.Photo, "Users/Uploads/OurCustomers");
            }

            employee.RedirectLink = model.RedirectLink;
            employee.ModifiedAt = DateTime.Now;

            _context.OurEmployees.Update(employee);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var employee = _context.OurEmployees.FirstOrDefault(x => x.Id == id);
            if (employee is null) return NotFound("Əməkdaş Tapılmadı!");

            employee.IsDeleted = true;
            _context.Update(employee);
            _context.SaveChanges();

            return Ok();
        }
        #endregion
    }
}
