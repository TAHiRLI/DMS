using DMS.az.Areas.Admin.ViewModels.Blogs;
using DMS.az.Areas.Admin.ViewModels.TeamMembers;
using DMS.az.DAL;
using DMS.az.Models;
using DMS.az.Utilities;
using DSM.az.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DMS.az.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IFileService _fileService;
        private readonly IEmailSender _emailSender;

        public BlogsController(AppDbContext context, IFileService fileService, IEmailSender emailSender)
        {
            _context = context;
            _fileService = fileService;
            _emailSender = emailSender;
        }

        #region BlogsList
        [HttpGet]
        public async Task<IActionResult> Index(BlogsIndexVM model)
        {
            model = new BlogsIndexVM
            {
                Blogs = await _context.Blogs.OrderByDescending(b => b.Id).Where(b => !b.IsDeleted).ToListAsync()
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
        public async Task<IActionResult> Create(BlogsCreateVM model)
        {
            if (!ModelState.IsValid) return View();

            var blog = new Blog
            {
                Photo = await _fileService.Upload(model.Photo, "Users/Uploads/Blogs"),
                Title = model.Title,
                Description = model.Description,
                ShortDesc = model.ShortDesc,
                PostDate = model.PostDate,
                CreatedAt = DateTime.Now
            };

            _context.Blogs.Add(blog);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Update(int id)
        {
            var blog = _context.Blogs.FirstOrDefault(b => b.Id == id);
            if (blog is null) return NotFound("Bloq Tapılmadı!");

            var model = new BlogsUpdateVM
            {
                PhotoPath = blog.Photo,
                Title = blog.Title,
                Description = blog.Description,
                ShortDesc = blog.ShortDesc,
                PostDate = blog.PostDate,
                ViewCount= blog.ViewCount,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, BlogsUpdateVM model)
        {
            var blog = _context.Blogs.FirstOrDefault(e => e.Id == id);
            if (blog is null) return NotFound("Bloq Tapılmadı!");


            if (!ModelState.IsValid) return View();

            if (model.Photo is not null)
            {
                _fileService.Delete(blog.Photo);
                blog.Photo = await _fileService.Upload(model.Photo, "Users/Uploads/Blogs");
            }

            blog.Title = model.Title;
            blog.Description = model.Description;
            blog.ShortDesc = model.ShortDesc;
            blog.PostDate = model.PostDate;
            blog.ViewCount = model.ViewCount;
            blog.ModifiedAt = DateTime.Now;

            _context.Blogs.Update(blog);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Details
        public async Task<IActionResult> Details(int id)
        {
            var blog = await _context.Blogs.FirstOrDefaultAsync(p => p.Id == id);
            if (blog == null) return NotFound("Bloq Tapılmadı!");

            var selectedBlog = new BlogsDetailsVM()
            {
                Title = blog.Title,
                Description = blog.Description,
                ShortDesc = blog.ShortDesc,
                Photo = blog.Photo,
                ViewCount = blog.ViewCount,
                CreatedAt = blog.CreatedAt,
                ModifiedAt = blog.ModifiedAt,
            };

            return View(selectedBlog);
        }
        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var blog = _context.Blogs.FirstOrDefault(x => x.Id == id);
            if (blog is null) return NotFound("Blog Not Found!");

            blog.IsDeleted = true;
            _context.Update(blog);
            _context.SaveChanges();

            return Ok();
        }
        #endregion

        public IActionResult ShareWithSubs(int id)
        {
            //var message = new Models.Message
            //{
            //    Content = "Blog Created"
            //};

            //_context.Messages.Add(message);
            //_context.SaveChanges();
            var blog = _context.Blogs.FirstOrDefault(x => x.Id == id);
            if (blog is null) return NotFound("Blog Not Found!");

            foreach (var subcriber in _context.Subscribers)
            {
                var emailMessage = new Utilities.Message(new[] { subcriber.Email }, "New Message", "New blog created", "dmsmessages@gmail.com");
                _emailSender.SendEmail(emailMessage, "subs", blog);
            }   

            blog.IsSent = true;
            _context.Update(blog);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
