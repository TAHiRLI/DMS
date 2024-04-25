using DMS.az.CustomValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace DMS.az.Areas.Admin.ViewModels.Blogs
{
    public class BlogsUpdateVM
    {
        public string? PhotoPath { get; set; }

        [AllowedMaxSize(2)]
        [AllowedFileTypes("image/jpeg", "image/png")]
        public IFormFile? Photo { get; set; }
        [Required(ErrorMessage = "Title must be included"), MinLength(3, ErrorMessage = "Title must be minimum 3 characters")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description must be included"), MinLength(3, ErrorMessage = "Decription must be minimum 3 characters")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Short Description must be included"), MinLength(3, ErrorMessage = "Short Decription must be minimum 3 characters"), MaxLength(70, ErrorMessage = "Short Decription must be maximum 70 characters")]
        public string ShortDesc { get; set; }
        [Required(ErrorMessage = "Post Date must be included")]
        public DateTime PostDate { get; set; }
        public int ViewCount { get; set; } = 1;
    }
}
