namespace DMS.az.Areas.Admin.ViewModels.CompanyHistory
{
    public class HistoryIndexVM
    {
        public HistoryIndexVM()
        {
            CompanyHistories = new List<Models.CompanyHistory>();
        }
        public List<Models.CompanyHistory> CompanyHistories { get; set; }
    }
}
