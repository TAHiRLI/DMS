using DMS.az.Models;

namespace DMS.az.Areas.Admin.ViewModels.Message
{
    public class MessageIndexVM
    {
        public MessageIndexVM()
        {
            Messages = new List<Models.Message>();
        }

        public List<Models.Message> Messages { get; set; }
        public bool IsChecked { get; set; }
    }
}
