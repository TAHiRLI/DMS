﻿using DMS.az.DAL;
using DMS.az.Models;
using DMS.az.Utilities;
using DMS.az.ViewModels.Contact;
using DMS.az.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DMS.az.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IEmailSender _emailSender;

        public HomeController(AppDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "Əsas Səhifə";
            ViewBag.ServicesCount = _context.Services.Count();

            var model = new HomeIndexVM()
            {
                //Sliders = await _context.Sliders.Where(x => !x.IsDeleted).ToListAsync(),
                Video = await _context.Video.FirstAsync(),
                AboutUs = await _context.AboutUs.Where(x => !x.IsDeleted).ToListAsync(),
                Portfolios = await _context.Portfolios.Where(x => !x.IsDeleted).ToListAsync(),
                Services = await _context.Services.Where(x => !x.IsDeleted).Take(4).ToListAsync(),
                Blogs = await _context.Blogs.OrderByDescending(b => b.Id).Where(x => !x.IsDeleted).Take(10).ToListAsync(),
                OurEmployees = await _context.OurEmployees.Where(x => !x.IsDeleted).ToListAsync(),
                Contact = await _context.Contact.ToListAsync(),

            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactIndexVM model)
        {
            if (!ModelState.IsValid)
            {
                TempData["UnSuccessMessage"] = "unsuccess";
                ViewBag.Title = "Əsas Səhifə";


                var model1 = new HomeIndexVM
                {
                    //Sliders = await _context.Sliders.Where(x => !x.IsDeleted).ToListAsync(),
                    AboutUs = await _context.AboutUs.Where(x => !x.IsDeleted).ToListAsync(),
                    Portfolios = await _context.Portfolios.Where(x => !x.IsDeleted).OrderByDescending(x=> x.CreatedAt).Take(20).ToListAsync(),
                    Services = await _context.Services.Where(x => !x.IsDeleted).OrderByDescending(x => x.Id).Take(3).ToListAsync(),
                    Contact = await _context.Contact.ToListAsync(),
                };

                return View(model1);
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

            var emailMessage = new Utilities.Message(new[] { "a.mirheyder004@gmail.com" }, "New Message", message.Content, message.SenderEmail);
            _emailSender.SendEmail(emailMessage, "contact");


            TempData["SuccessMessage"] = "Mesaj uğurla göndərildi";


            ViewBag.ServicesCount = _context.Services.Count();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> LoadMore(int skipRow)
        {

            var model = new HomeLoadMoreVM
            {
                Services = await _context.Services.OrderByDescending(x => x.Id).Skip(3 * skipRow).Take(3).ToListAsync()
            };

            return PartialView("_ServicesComponentPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(string email)
        {

            var existingSubscriber = await _context.Subscribers.FirstOrDefaultAsync(x => x.Email.ToLower() == email.Trim().ToLower());

            if (existingSubscriber == null)
            {
                return RedirectToAction("index", "home");
            };

            var Subscriber = new Subscriber()
            {
                Email = email,
            };

            _context.Subscribers.Add(Subscriber);
            _context.SaveChanges();

            return RedirectToAction("index", "home");
        }
    }
}
