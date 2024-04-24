using System.ComponentModel.DataAnnotations;

namespace DMS.az.Areas.Admin.ViewModels.TeamMembers
{
    public class TeamMemberUpdateVM
    {
        public string? PhotoPath { get; set; }
        public IFormFile? Photo { get; set; }
        [Required(ErrorMessage = "Ad daxil edilməlidir"), MinLength(3, ErrorMessage = "Ad minimum 3 simvol olmalıdır")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Soyad daxil edilməlidir"), MinLength(3, ErrorMessage = "Soyad minimum 3 simvol olmalıdır")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Təsvir daxil edilməlidir"), MinLength(3, ErrorMessage = "Təsvir minimum 10 simvol olmalıdır"), MaxLength(120, ErrorMessage = "Təsvir maksimum 120 simvol olmalıdır")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Vəzifə daxil edilməlidir")]
        public string Duty { get; set; }
    }
}
