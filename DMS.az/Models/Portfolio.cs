using DMS.az.Models.Base;

namespace DMS.az.Models
{
    public class Portfolio : BaseEntity
    {
        public string Photo { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public PortfolioCategory PortfolioCategory { get; set; }
        public int PortfolioCategoryId { get; set; }
        public string? SEO { get; set; }
        public DateTime PostDate { get; set; }
    }
}
