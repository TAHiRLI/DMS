using DMS.az.Models.Base;

namespace DMS.az.Models
{
    public class OurEmployee : BaseEntity
    {
        public string Photo { get; set; }
        public string? RedirectLink { get; set; }
    }
}
