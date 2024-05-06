using DMS.az.Models.Base;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace DMS.az.Models
{
    public class TeamMember : BaseEntity
    {
        public string Photo { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Duty { get; set; }
        public string Description { get; set; }
        public string? Instagram { get; set; } = "#";
        public string? LinkedIn { get; set; } = "#";
        public string? Facebook { get; set; } = "#";

    }
}
