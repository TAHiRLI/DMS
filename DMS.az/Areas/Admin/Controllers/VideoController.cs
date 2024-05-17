using DMS.az.Areas.Admin.ViewModels.Contact;
using DMS.az.Areas.Admin.ViewModels.Video;
using DMS.az.DAL;
using DMS.az.Models;
using DSM.az.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace DMS.az.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin, SuperAdmin")]
    public class VideoController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IFileService _fileService;

        public VideoController(AppDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }


        #region GetVideo
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new VideoIndexVM()
            {
                Video = await _context.Video.OrderByDescending(v => v.Id).ToListAsync()
            };

            return View(model);
        }
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Update(int id)
        {
            var video = _context.Video.FirstOrDefault(p => p.Id == id);
            if (video is null) return NotFound("Video Not Found");

            var model = new VideoUpdateVM
            {
                Link = video.Link,
                PhotoPath = video.CoverPhoto,
                ModifiedAt = video.ModifiedAt,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, VideoUpdateVM model)
        {
            var video = _context.Video.FirstOrDefault(s => s.Id == id);
            if (video is null) return NotFound("Video Not Found!");


            if (!ModelState.IsValid) return View();

            if (model.Photo is not null)
            {
                _fileService.Delete(video.CoverPhoto);
                video.CoverPhoto = await _fileService.Upload(model.Photo, "Users/Uploads/Video");
            }

            video.Link = model.Link;
            video.ModifiedAt = DateTime.Now;

            _context.Video.Update(video);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        #endregion

    }
}
