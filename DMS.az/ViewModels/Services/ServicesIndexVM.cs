using DMS.az.Models;

namespace DMS.az.ViewModels.Services
{
	public class ServicesIndexVM
	{
        public ServicesIndexVM()
		{
			Services = new List<Service>();
        }

		public List<Service> Services { get; set; }
	}
}
