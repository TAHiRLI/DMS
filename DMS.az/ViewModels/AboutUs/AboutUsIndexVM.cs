namespace DMS.az.ViewModels.AboutUs
{
    public class AboutUsIndexVM
    {
        public AboutUsIndexVM()
        {
            AboutUs = new List<Models.AboutUs>();
            TeamMembers = new List<Models.TeamMember>();
            CompanyHistories = new List<Models.CompanyHistory>();
        }

        public List<Models.AboutUs> AboutUs { get; set; }
        public List<Models.TeamMember> TeamMembers { get; set; }
        public List<Models.CompanyHistory> CompanyHistories { get; set; }
    }
}
