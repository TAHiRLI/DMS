using DMS.az.Models;
using DSM.az.Models;
using System.ComponentModel.DataAnnotations;

namespace DMS.az.ViewModels.Home
{
    public class HomeIndexVM
    {
        //public List<Slider> Sliders { get; set; }
        public Video Video { get; set; }
        public List<Models.AboutUs> AboutUs { get; set; }
        public List<Models.Portfolio> Portfolios { get; set; }
        public List<Service> Services { get; set; }
        public List<OurEmployee> OurEmployees { get; set; }
        public List<Models.Contact> Contact { get; set; }

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
        [Required(ErrorMessage = "You should enter message")]

        public string Content { get; set; }
    }
}
