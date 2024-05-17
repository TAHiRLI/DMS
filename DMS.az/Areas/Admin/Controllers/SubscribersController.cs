using DMS.az.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DMS.az.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SubscribersController : Controller
    {
        private readonly AppDbContext _context;

        public SubscribersController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var subscribers = _context.Subscribers.ToList();
            return View(subscribers);
        }
        public IActionResult Delete(int id)
        {
            var subscriber = _context.Subscribers.SingleOrDefault(x => x.Id == id);
            if (subscriber == null)
            {
                return NotFound();
            }

            _context.Subscribers.Remove(subscriber);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
