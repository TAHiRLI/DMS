namespace DMS.az.Areas.Admin.ViewModels.Portfolio
{
    public class PortfolioDetailsVM
    {
        public string Photo { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string ShortDesc { get; set; }
        public Models.PortfolioCategory PortfolioCategory { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

    }
}
