using System.ComponentModel.DataAnnotations;

namespace DMS.az.ViewModels.Contact
{
    public class MessageVM
    {
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters")]
        [MaxLength(38, ErrorMessage = "Name must be maximum 38 characters")]
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string SenderName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Email format is not correct")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string SenderEmail { get; set; }
        public string Content { get; set; }
    }
}
