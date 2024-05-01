using DMS.az.Models;

namespace DMS.az.Areas.Admin.ViewModels.OurEmployees
{
    public class OurEmployeesIndexVM
    {
        public OurEmployeesIndexVM()
        {
            Clients = new List<Models.Client>();
        }

        public List<Client> Clients { get; set; }
    }
}
