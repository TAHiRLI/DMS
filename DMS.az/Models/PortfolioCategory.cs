using DMS.az.Models.Base;

namespace DMS.az.Models
{
    public class PortfolioCategory : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Portfolio> Portfolios { get; set; }
    }
}
