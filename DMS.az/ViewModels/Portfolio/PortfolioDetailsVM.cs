using DMS.az.Models;

namespace DMS.az.ViewModels.Portfolio
{
    public class PortfolioDetailsVM
    {
        public Models.Portfolio Portfolio { get; set; }
        public ICollection<Models.Blog> Blogs { get; set; }

    }
}
