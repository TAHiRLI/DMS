using DMS.az.Models.Base;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace DMS.az.Models
{
    public class CompanyHistory : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public string EventDateAsString { get; set; }
    }
}
