namespace DMS.az.Areas.Admin.ViewModels.Portfolio
{
    public class PortfolioIndexVM
    {
        public PortfolioIndexVM()
        {
            Portfolios = new List<Models.Portfolio>();
        }

        public List<Models.Portfolio> Portfolios { get; set; }
    }
}
