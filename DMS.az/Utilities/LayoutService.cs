using DMS.az.DAL;
using DMS.az.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DMS.az.Utilities
{
    public class LayoutService:ILayoutService
    {
        private readonly AppDbContext _context;

        public LayoutService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Contact>?  GetContact()
        {
            return await _context.Contact.FirstAsync();
        }
        public async Task<List<Service>> GetServices()
        {
            return await _context.Services.Take(6).Where(x=> !x.IsDeleted).ToListAsync();
        }
    
    }
}
