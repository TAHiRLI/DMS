namespace DMS.az.Areas.Admin.ViewModels.TeamMembers
{
    public class TeamMemberIndexVM
    {
        public TeamMemberIndexVM()
        {
            TeamMembers = new List<Models.TeamMember>();
        }
        public List<Models.TeamMember> TeamMembers { get; set; }
    }
}
