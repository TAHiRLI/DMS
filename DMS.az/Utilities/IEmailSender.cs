using DMS.az.Models;

namespace DMS.az.Utilities
{
    public interface IEmailSender
    {
        void SendEmail(Message message, string category, Blog? blog = null);
    }
}
