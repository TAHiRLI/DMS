using DMS.az.Models;
using DSM.az.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DMS.az.DAL
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<PortfolioCategory> PortfolioCategories { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<OurEmployee> OurEmployees { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Blog> Blogs { get; set; }

    }
}
