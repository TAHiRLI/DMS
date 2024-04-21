using DMS.az.DAL;
using DMS.az.Models;
using DMS.az.ViewModels.Contact;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System.Net.Mail;
using System.Net;
using DMS.az.Utilities;

namespace DMS.az.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IEmailSender _emailSender;

        public ContactController(AppDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "Əlaqə";

            var model = new ContactIndexVM
            {
                Contact = await _context.Contact.ToListAsync()
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(ContactIndexVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Contact = await _context.Contact.ToListAsync();
                return View(model);
            }


            var message = new Models.Message
            {
                Content = model.Content,
                SenderEmail = model.SenderEmail,
                SenderName = model.SenderName,
                CreatedAt = DateTime.UtcNow,
            };

            _context.Messages.Add(message);
            _context.SaveChanges();

            var emailMessage = new Utilities.Message(new[] { "İnfo@dms.az" }, "New Message", message.Content, message.SenderEmail);
            _emailSender.SendEmail(emailMessage);


            TempData["SuccessMessage"] = "Mesaj uğurla göndərildi";

            model = new ContactIndexVM
            {
                Contact = await _context.Contact.ToListAsync(),
            };


            return RedirectToAction(nameof(Index));
        }

    }
}
