using DMS.az.DAL;
using DSM.az.Areas.Admin.ViewModels.Slider;
using DSM.az.Models;
using DSM.az.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Diagnostics.Internal;

namespace DSM.az.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IFileService _fileService;

        public SliderController(AppDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        #region SliderList
        [HttpGet]
        public async Task<IActionResult> Index(SliderIndexVM model)
        {
            model = new SliderIndexVM
            {
                Sliders = await _context.Sliders.OrderByDescending(s => s.Id).Where(s => !s.IsDeleted).ToListAsync(),
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
        public async Task<IActionResult> Create(SliderCreateVM model)
        {
            if (!ModelState.IsValid) { return View(model); }

            if (model.Type.ToString() == "Photo")
            {
                if (!_fileService.IsImage(model.MediaName))
                {
                    ModelState.AddModelError("MediaName", "Fayl formatı yalnışdır");
                    return View();
                }

                if (!_fileService.IsBiggerThanSize(model.MediaName, 80000))
                {
                    ModelState.AddModelError("MediaName", "Faylın ölçüsü 2MB-dan böyükdür");
                    return View();
                }

                var sliderByType = await _context.Sliders.FirstOrDefaultAsync(s => s.Type == DMS.az.Enums.SliderContentType.Video && !s.IsDeleted);

                if (sliderByType is not null)
                {
                    ModelState.AddModelError("MediaName", "Fayl formatı uyğun deyil");
                    return View();
                }
            }

            if (model.Type.ToString() == "Video")
            {
                if (!_fileService.IsVideo(model.MediaName))
                {
                    ModelState.AddModelError("MediaName", "Fayl formatı yalnışdır");
                    return View();
                }

                if (!_fileService.IsBiggerThanSize(model.MediaName, 5000))
                {
                    ModelState.AddModelError("MediaName", "Faylın ölçüsü 5MB-dan böyükdür");
                    return View();
                }

                var sliderByType = await _context.Sliders.FirstOrDefaultAsync(s => s.Type == DMS.az.Enums.SliderContentType.Photo && !s.IsDeleted);

                if (sliderByType is not null)
                {
                    ModelState.AddModelError("MediaName", "Fayl formatı uyğun deyil");
                    return View();
                }
            }

            var slider = new Slider
            {
                Title = model.Title,
                Type = model.Type,
                MediaPath = await _fileService.Upload(model.MediaName, "Users/images"),
                CreatedAt = DateTime.Now
            };

            _context.Sliders.Add(slider);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Update(int id)
        {
            var slider = _context.Sliders.FirstOrDefault(s => s.Id == id);
            if (slider is null) return NotFound("Slider Tapılmadı");

            var model = new SliderUpdateVM
            {
                Title = slider.Title,
                Type = slider.Type,
                MediaPath = slider.MediaPath,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, SliderUpdateVM model)
        {
            var slider = _context.Sliders.FirstOrDefault(s => s.Id == id);
            if (slider is null) return NotFound("Slider Tapılmadı");


            if (!ModelState.IsValid) return View();

            if (model.MediaName is not null)
            {
                if (model.Type.ToString() == "Photo")
                {
                    if (!_fileService.IsImage(model.MediaName))
                    {
                        ModelState.AddModelError("MediaName", "Fayl formatı yalnışdır");
                        return View();
                    }

                    if (!_fileService.IsBiggerThanSize(model.MediaName, 2000))
                    {
                        ModelState.AddModelError("MediaName", "Faylın ölçüsü 2MB-dan böyükdür");
                        return View();
                    }

                    var sliderByType = await _context.Sliders.Where(s => s.Id != id).FirstOrDefaultAsync(s => s.Type == DMS.az.Enums.SliderContentType.Video && !s.IsDeleted );

                    if (sliderByType is not null)
                    {
                        ModelState.AddModelError("MediaName", "Fayl formatı uyğun deyil");
                        return View();
                    }
                }

                if (model.Type.ToString() == "Video")
                {
                    if (!_fileService.IsVideo(model.MediaName))
                    {
                        ModelState.AddModelError("MediaName", "Fayl formatı yalnışdır");
                        return View();
                    }

                    if (!_fileService.IsBiggerThanSize(model.MediaName, 5000))
                    {
                        ModelState.AddModelError("MediaName", "Faylın ölçüsü 5MB-dan böyükdür");
                        return View();
                    }

                    var sliderByType = await _context.Sliders.Where(s => s.Id != id).FirstOrDefaultAsync(s => s.Type == DMS.az.Enums.SliderContentType.Photo && !s.IsDeleted);

                    if (sliderByType is not null)
                    {
                        ModelState.AddModelError("MediaName", "Fayl formatı uyğun deyil");
                        return View();
                    }
                }


                _fileService.Delete(slider.MediaPath);
                slider.MediaPath =  await _fileService.Upload(model.MediaName, "Users/images");
            }

            slider.Title = model.Title;
            slider.Type = model.Type;
            slider.ModifiedAt = DateTime.Now;

            _context.Sliders.Update(slider);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        #endregion

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var slider = _context.Sliders.FirstOrDefault(s => s.Id == id && !s.IsDeleted);
            if (slider is null) NotFound("Slider Tapılmadı!");

            slider.IsDeleted = true;
            _context.Update(slider);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


    }
}
