using DMS.az.Models;

namespace DMS.az.Utilities
{
    public interface ILayoutService
    {
        Task<Contact> GetContact();
        Task<List<Service>> GetServices();
    }
}
