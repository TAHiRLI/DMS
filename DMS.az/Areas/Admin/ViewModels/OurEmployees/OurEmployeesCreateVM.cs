using System.ComponentModel.DataAnnotations;

namespace DMS.az.Areas.Admin.ViewModels.OurEmployees
{
    public class OurEmployeesCreateVM
    {
        [Required]
        public IFormFile Photo { get; set; }
        public string? RedirectLink { get; set; }
    }
}
