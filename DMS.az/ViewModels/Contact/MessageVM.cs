using System.ComponentModel.DataAnnotations;

namespace DMS.az.ViewModels.Contact
{
    public class MessageVM
    {
        [MinLength(3, ErrorMessage = "Ad ən az 3 simvol olmalıdır")]
        [MaxLength(38, ErrorMessage = "Ad 30 simvoldan çox ola bilməz")]
        [Required(ErrorMessage = "Ad daxil edilməlidir")]
        [Display(Name = "Name")]
        public string SenderName { get; set; }
        [Required(ErrorMessage = "Email daxil edilməlidir")]
        [EmailAddress(ErrorMessage = "Email formatı düzgün deyil")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string SenderEmail { get; set; }
        [Required(ErrorMessage = "Telefon nömrəsi daxil edilməlidir")]
        public string SenderPhoneNumber { get; set; }
        public string Content { get; set; }
    }
}
