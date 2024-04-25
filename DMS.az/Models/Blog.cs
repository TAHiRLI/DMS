using DMS.az.Models.Base;

namespace DMS.az.Models
{
    public class Blog : BaseEntity
    {
        public string Photo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
