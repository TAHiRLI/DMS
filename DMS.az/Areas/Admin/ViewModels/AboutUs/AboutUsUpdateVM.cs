using System.ComponentModel.DataAnnotations;

namespace DMS.az.Areas.Admin.ViewModels.AboutUs
{
    public class AboutUsUpdateVM
    {
        public string? PhotoName1 { get; set; }
        public IFormFile? Photo1 { get; set; }
        public string? PhotoName2 { get; set; }
        public IFormFile? Photo2 { get; set; }
        public string? PhotoName3 { get; set; }
        public IFormFile? Photo3 { get; set; }


        [Required(ErrorMessage = "Təsvir daxil edilməlidir")]
        public string Description { get; set; }
    }
}
