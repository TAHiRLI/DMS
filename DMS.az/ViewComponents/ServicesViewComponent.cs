using DMS.az.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace DMS.az.ViewComponents
{
    public class ServicesViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public ServicesViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var services = _context.Services.Where(s => !s.IsDeleted).ToList();
            return View(services);
        }
    }
}
