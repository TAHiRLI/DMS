using DMS.az.Areas.Admin.ViewModels.Contact;
using DMS.az.DAL;
using DMS.az.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace DMS.az.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }

        #region ContactList
        [HttpGet]
        public async Task<IActionResult> Index(ContactIndexVM model)
        {
            model = new ContactIndexVM
            {
                Contact = await _context.Contact.OrderByDescending(s => s.Id).Where(s => !s.IsDeleted).ToListAsync(),
            };

            return View(model);
        }
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Update(int id)
        {
            var contact = _context.Contact.FirstOrDefault(p => p.Id == id);
            if (contact is null) return NotFound("Kontakt Tapılmadı");

            var model = new ContactUpdateVM
            {
                Address = contact.Address,
                PhoneNumber = contact.PhoneNumber,
                Email = contact.Email,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, ContactUpdateVM model)
        {
            var contact = _context.Contact.FirstOrDefault(s => s.Id == id);
            if (contact is null) return NotFound("Kontakt Tapılmadı!");


            if (!ModelState.IsValid) return View();

            contact.Address = model.Address;
            contact.PhoneNumber = model.PhoneNumber;
            contact.Email = model.Email;

            _context.Contact.Update(contact);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        #endregion

    }
}
