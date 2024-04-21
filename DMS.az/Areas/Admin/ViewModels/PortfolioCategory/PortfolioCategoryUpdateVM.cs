using System.ComponentModel.DataAnnotations;

namespace DMS.az.Areas.Admin.ViewModels.PortfolioCategory
{
    public class PortfolioCategoryUpdateVM
    {
        [Required(ErrorMessage = "Ad daxil edilməlidir"), MinLength(3, ErrorMessage = "Ad minimum 3 simvol olmalıdır")]
        public string Name { get; set; }
    }
}
