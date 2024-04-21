using Microsoft.AspNetCore.Identity;

namespace DSM.az.Models
{
    public class User : IdentityUser
    {
        public string? FullName { get; set; }
    }
}
