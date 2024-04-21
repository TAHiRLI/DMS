using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace DMS.az.Areas.Admin.ViewModels.Portfolio
{
    public class PortfolioCreateVM
    {
        [Required(ErrorMessage ="Şəkil daxil edilməlidir")]
        public IFormFile Photo { get; set; }
        //[Required(ErrorMessage = "Başlıq daxil edilməlidir"), MinLength(3, ErrorMessage = "Başlıq minimum 3 simvol olmalıdır")]
        public string? Title { get; set; }
        //[Required(ErrorMessage = "Təsvir daxil edilməlidir"), MinLength(3, ErrorMessage = "Təsvir minimum 3 simvol olmalıdır")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Qısa təsvir daxil edilməlidir"), MinLength(3, ErrorMessage = "qısa təsvir minimum 3 simvol olmalıdır")]
        public string ShortDesc { get; set; }
        [Required(ErrorMessage = "Kateqoriya daxil edilməlidir")]
        public int PortfolioCategoryId { get; set; }
        public List<SelectListItem>? PortfolioCategories { get; set; }
    }
}
