using System.ComponentModel.DataAnnotations;

namespace DMS.az.Areas.Admin.ViewModels.CompanyHistory
{
    public class HistoryUpdateVM
    {
        [Required(ErrorMessage = "Title must be included"), MinLength(3, ErrorMessage = "Title must be minimum 3 characters")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description must be included"), MinLength(10, ErrorMessage = "Decription must be minimum 10 characters"), MaxLength(170, ErrorMessage = "Decription must be maximum 170 characters")]
        public string Description { get; set; }
        [Required]
        public DateTime EventDate { get; set; }
        [Required]
        public string EventDateAsString { get; set; }
    }
}
