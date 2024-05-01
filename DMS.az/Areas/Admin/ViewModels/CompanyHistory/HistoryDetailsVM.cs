namespace DMS.az.Areas.Admin.ViewModels.CompanyHistory
{
    public class HistoryDetailsVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public string EventDateAsString { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
