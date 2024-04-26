using DMS.az.DAL;
using DMS.az.Models;
using DMS.az.Utilities.Pagination;
using DMS.az.ViewModels.Blogs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DMS.az.Controllers
{
    public class BlogsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IPaginator _paginator;

        public BlogsController(AppDbContext context, IPaginator paginator)
        {
            _context = context;
            _paginator = paginator;
        }
        public async Task<IActionResult> Index(BlogIndexVM model)
        {
            var blogs = await _context.Blogs.OrderByDescending(b => b.Id).Where(b => !b.IsDeleted).ToListAsync();

             model = new BlogIndexVM
            {
                Blogs = _paginator.GetPagedList(blogs, model.CurrentPage, model.PageSize),
            };

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var blogs = await _context.Blogs.Where(b => !b.IsDeleted).OrderByDescending(x => x.Id).Take(4).ToListAsync();
            var blog = _context.Blogs.Where(b => !b.IsDeleted).FirstOrDefault(x => x.Id == id);
            if (blog == null)
            {
                return NotFound();
            }
            blog.ViewCount++;

            _context.Blogs.Update(blog);
            _context.SaveChanges();

            var model = new BlogDetailsVM()
            {
                Blog = blog,
                Blogs = blogs
            };

            return View(model);
        }
    }
}
