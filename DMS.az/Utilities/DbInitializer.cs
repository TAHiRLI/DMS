using DMS.az.DAL;
using DMS.az.Models;
using DMS.az.Enums;
using DSM.az.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DSM.az.Utilities
{
    public class DbInitializer
    {
        public async static Task SeedAsync(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, AppDbContext context)
        {
            await SeedRolesAsync(roleManager);
            await SeedUsersAsync(userManager);
            await SeedContactAsync(context);
            await SeedAboutUsAsync(context);
        }

        private static async Task SeedAboutUsAsync(AppDbContext context)
        {
            var aboutUs = await context.AboutUs.Where(x => !x.IsDeleted).ToListAsync();
            if (aboutUs.Count == 0)
            {
                var newAboutUs = new AboutUs()
                {
                    Photo1 = "asdasd",
                    CreatedAt = DateTime.Now,
                };

                context.AboutUs.Add(newAboutUs);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedContactAsync(AppDbContext context)
        {
            var contact = await context.Contact.Where(x => !x.IsDeleted).ToListAsync();

            if (contact.Count == 0)
            {
                var newContact = new Contact()
                {
                    Address = "Lenkeran",
                    Email = "a.mirheyder004@gmail.com",
                    PhoneNumber = "1234567890", 
                    CreatedAt = DateTime.Now,
                };

                context.Contact.Add(newContact);
                await context.SaveChangesAsync();
            }


        }


        private async static Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in Enum.GetValues<UserRoles>())
            {
                if (!await roleManager.RoleExistsAsync(role.ToString()))
                {
                    await roleManager.CreateAsync(new IdentityRole
                    {
                        Name = role.ToString(),
                    });
                }
            }
        }

        private async static Task SeedUsersAsync(UserManager<User> userManager)
        {
            var user = await userManager.FindByNameAsync("Admin");

            if (user is null)
            {
                user = new User
                {
                    UserName = "Admin",
                    FullName = "Admin",
                    Email = "admin12@gmail.com",
                };
                var result = await userManager.CreateAsync(user, "admin1212");
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        throw new Exception(error.Description);
                    }
                }

                await userManager.AddToRoleAsync(user, UserRoles.Admin.ToString());
            }
        }
    }
}
