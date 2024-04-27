using DMS.az.CustomValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace DMS.az.Areas.Admin.ViewModels.Services
{
    public class ServiceCreateVM
    {
        [Required(ErrorMessage = "Icon must be added")]
        public IFormFile Icon { get; set; }
        [Required(ErrorMessage = "Photo must be added")]
        [AllowedMaxSize(2)]
        [AllowedFileTypes("image/jpeg", "image/png")]
        public IFormFile Photo { get; set; }

        [Required(ErrorMessage = "Name must be added")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Service Qualification must be added")]
        public string ServiceQualification { get; set; }


        [Required(ErrorMessage = "Description must be added")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Short Description must be added")]
        public string ShortDesc { get; set; }
    }
}
