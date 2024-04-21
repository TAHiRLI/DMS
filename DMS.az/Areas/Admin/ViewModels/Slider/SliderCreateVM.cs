using DMS.az.Enums;
using System.ComponentModel.DataAnnotations;

namespace DSM.az.Areas.Admin.ViewModels.Slider
{
    public class SliderCreateVM
    {
        [Required(ErrorMessage = "Fayl daxil edilməlidir")]
        public IFormFile MediaName { get; set; }

        [MinLength(5, ErrorMessage = "Başlıq minimum 5 simvol olmalıdır")]
        [MaxLength(60, ErrorMessage = "Başlıq maksimum 60 simvol olmalıdır")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Fayl tipi daxil edilməlidir")]
        public SliderContentType Type { get; set; }

    }
}
