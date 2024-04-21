namespace DMS.az.Areas.Admin.ViewModels.PortfolioCategory
{
    public class PortfolioCategoryIndexVM
    {
        public PortfolioCategoryIndexVM()
        {
            PortfolioCategories = new List<Models.PortfolioCategory>();
        }

        public List<Models.PortfolioCategory> PortfolioCategories { get; set; }
    }
}
