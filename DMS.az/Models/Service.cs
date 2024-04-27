using DMS.az.Models.Base;

namespace DMS.az.Models
{
    public class Service : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDesc { get; set; }
        public string ServiceQualification { get; set; }
        public string Icon { get; set; }
        public string Photo { get; set; }

    }
}
