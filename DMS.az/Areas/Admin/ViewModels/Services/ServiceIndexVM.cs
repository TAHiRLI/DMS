using DMS.az.Models;

namespace DMS.az.Areas.Admin.ViewModels.Services
{
    public class ServiceIndexVM
    {
        public ServiceIndexVM()
        {
            Services = new List<Models.Service>();
        }

        public List<Service> Services { get; set; }
    }
}
