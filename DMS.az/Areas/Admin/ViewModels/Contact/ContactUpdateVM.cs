using System.ComponentModel.DataAnnotations;

namespace DMS.az.Areas.Admin.ViewModels.Contact
{
    public class ContactUpdateVM
    {
        [Required(ErrorMessage ="Adres daxil edilməlidir")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Telefon daxil edilməlidir")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email daxil edilməlidir")]
        public string Email { get; set; }
    }
}
