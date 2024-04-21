using DMS.az.Areas.Admin.ViewModels.Message;
using DMS.az.Areas.Admin.ViewModels.Portfolio;
using DMS.az.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DMS.az.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MessageController : Controller
    {
        private readonly AppDbContext _context;

        public MessageController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(MessageIndexVM model)
        {
            if (model.IsChecked == true)
            {
                model.Messages = await _context.Messages.Where(m => !m.IsOpened).OrderByDescending(m => m.Id).ToListAsync();
                return View(model);
            }

            model.Messages = await _context.Messages.OrderByDescending(m => m.Id).ToListAsync();
            return View(model);
        }


        public async Task<IActionResult> Details(int id)
        {
            var message = await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);
            if (message == null) return NotFound("Mesaj Tapılmadı!");

            var selectedMessage = new MessageDetailsVM()
            {
                SenderName = message.SenderName,
                SenderEmail = message.SenderEmail,
                Content = message.Content,
                CreatedAt = message.CreatedAt,
            };

            message.IsOpened = true;
            _context.Messages.Update(message);
            _context.SaveChanges();

            return View(selectedMessage);
        }
    }
}
