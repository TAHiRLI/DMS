namespace DMS.az.Areas.Admin.ViewModels.AboutUs
{
    public class AboutUsIndexVM
    {
        public AboutUsIndexVM()
        {
            AboutUs = new List<Models.AboutUs>();
        }

        public List<Models.AboutUs> AboutUs { get; set; }
    }
}
