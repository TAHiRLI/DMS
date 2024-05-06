using DMS.az.Models.Base;

namespace DMS.az.Models
{
    public class Blog : BaseEntity
    {
        public string Photo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDesc { get; set; }
        public DateTime PostDate { get; set; }
        public bool IsSent { get; set; }

        public int ViewCount { get; set; } = 1;

    }
}
