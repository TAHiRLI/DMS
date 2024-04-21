using System.ComponentModel.DataAnnotations;

namespace DSM.az.Areas.Admin.ViewModels.Account
{
    public class AccountLoginVM
    {
        [Required]
        public string Email { get; set; }
        public string? Fullname { get; set; }

        [Required]
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
