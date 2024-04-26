using DMS.az.CustomValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace DMS.az.Areas.Admin.ViewModels.Video
{
    public class VideoUpdateVM
    {
        [Required(ErrorMessage = "Title must be included")]
        public string Link { get; set; }
        public string? PhotoPath { get; set; }
        [AllowedMaxSize(2)]
        [AllowedFileTypes("image/jpeg", "image/png")]
        public IFormFile? Photo { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
