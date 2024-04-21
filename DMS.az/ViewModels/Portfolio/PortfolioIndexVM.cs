using DMS.az.Models;

namespace DMS.az.ViewModels.Portfolio
{
    public class PortfolioIndexVM
    {
        public PortfolioIndexVM()
        {
            Portfolio = new List<Models.Portfolio>();
            PortfolioCategories = new List<Models.PortfolioCategory>();
        }

        public List<Models.Portfolio> Portfolio { get; set; }
        public List<PortfolioCategory> PortfolioCategories { get; set; }
    }
}
