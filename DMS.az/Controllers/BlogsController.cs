using DMS.az.DAL;
using DMS.az.ViewModels.Blogs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DMS.az.Controllers
{
    public class BlogsController : Controller
    {
        private readonly AppDbContext _context;

        public BlogsController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var model = new BlogIndexVM
            {
                Blogs = await _context.Blogs.OrderByDescending(b => b.Id).Where(b => !b.IsDeleted).ToListAsync(),
            };

            return View(model);
        }
        public IActionResult Details(int id)
        {
            return View();
        }
    }
}
