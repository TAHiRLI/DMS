using MimeKit;

namespace DMS.az.Utilities
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string SenderEmail { get; set; }
        public Message(IEnumerable<string> to, string subject, string content, string senderEmail)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress(x)));
            Subject = subject;
            Content = content;
            SenderEmail = senderEmail; 
        }
    }
}
