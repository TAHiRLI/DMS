using DMS.az.Models.Base;

namespace DMS.az.Models
{
    public class Message : BaseEntity
    {
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string SenderPhoneNumber { get; set; }
        public string Content { get; set; }
        public bool IsOpened { get; set; }
    }
}
