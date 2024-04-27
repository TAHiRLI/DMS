using DMS.az.CustomValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace DMS.az.Areas.Admin.ViewModels.Services
{
    public class ServiceUpdateVM
    {
        public IFormFile? Icon { get; set; }
        public string? İconPath { get; set; }
        [AllowedMaxSize(2)]
        [AllowedFileTypes("image/jpeg", "image/png")]
        public IFormFile? Photo { get; set; }
        public string? PhotoName { get; set; }

        [Required(ErrorMessage = "Ad daxil edilməlidir")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Təsvir daxil edilməlidir")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Service Qualification must be added")]
        public string ServiceQualification { get; set; }
        [Required(ErrorMessage = "Qısa təsvir daxil edilməlidir")]
        public string ShortDesc { get; set; }
    }
}
