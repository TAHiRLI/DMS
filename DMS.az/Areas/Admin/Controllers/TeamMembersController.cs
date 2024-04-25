using DMS.az.Areas.Admin.ViewModels.OurEmployees;
using DMS.az.Areas.Admin.ViewModels.TeamMembers;
using DMS.az.DAL;
using DMS.az.Models;
using DSM.az.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DMS.az.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamMembersController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IFileService _fileService;

        public TeamMembersController(AppDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        #region TeamMembersList
        [HttpGet]
        public async Task<IActionResult> Index(TeamMemberIndexVM model)
        {
            model = new TeamMemberIndexVM
            {
                 TeamMembers = await _context.TeamMembers.OrderByDescending(m => m.Id).Where(m => !m.IsDeleted).ToListAsync()
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
        public async Task<IActionResult> Create(TeamMemberCreateVM model)
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

            var teamMember = new TeamMember
            {
                Photo = await _fileService.Upload(model.Photo, "Users/Uploads/Team"),
                Name = model.Name,
                Surname = model.Surname,
                Description = model.Description,
                Duty = model.Duty,
                CreatedAt = DateTime.Now
            };

            _context.TeamMembers.Add(teamMember);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Update(int id)
        {
            var teamMember = _context.TeamMembers.FirstOrDefault(e => e.Id == id);
            if (teamMember is null) return NotFound("Əməkdaş Tapılmadı!");

            var model = new TeamMemberUpdateVM
            {
                PhotoPath = teamMember.Photo,
                Name = teamMember.Name,
                Surname = teamMember.Surname,
                Description = teamMember.Description,
                Duty = teamMember.Duty
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, TeamMemberUpdateVM model)
        {
            var teamMember = _context.TeamMembers.FirstOrDefault(e => e.Id == id);
            if (teamMember is null) return NotFound("Əməkdaş Tapılmadı!");


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
                _fileService.Delete(teamMember.Photo);
                teamMember.Photo = await _fileService.Upload(model.Photo, "Users/Uploads/Team");
            }

            teamMember.Name = model.Name;
            teamMember.Surname = model.Surname;
            teamMember.Description = model.Description;
            teamMember.Duty = model.Duty;
            teamMember.ModifiedAt = DateTime.Now;

            _context.TeamMembers.Update(teamMember);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        #endregion

    }
}
