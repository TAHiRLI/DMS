using DMS.az.DAL;
using DMS.az.Models;
using MailKit.Net.Smtp;
using MimeKit;
using System.Text;

namespace DMS.az.Utilities
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfiguration;


        public EmailSender(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        public void SendEmail(Message message, string category, Blog? blog)
        {
            var emailMessage = CreateEmailMessage(message, category, blog);
            Send(emailMessage);
        }

        //private MimeMessage CreateEmailMessage(Message message, string category)
        //{
        //    var emailMessage = new MimeMessage();
        //    emailMessage.From.Add(new MailboxAddress(_emailConfiguration.UserName));
        //    emailMessage.To.AddRange(message.To);
        //    emailMessage.Subject = message.Subject;



        //    var contentWithSenderEmail = ""; ;

        //    //var blog = _context.Blogs.FirstOrDefault(x => x.Id == );
        //    if (category == "contact")
        //    {
        //        contentWithSenderEmail = $"{message.Content} - Sender's Email: {message.SenderEmail}";
        //    }
        //    else
        //    {
        //        contentWithSenderEmail = $"{"subs"} - ";
        //    }

        //    emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = contentWithSenderEmail };
        //    return emailMessage;
        //}

        private MimeMessage CreateEmailMessage(Message message, string category, Blog blog)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfiguration.UserName));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            // Initialize HTML content
            var htmlContent = new StringBuilder();

            // Construct HTML content
            htmlContent.AppendLine("<html>");
            htmlContent.AppendLine("<body>");
            htmlContent.AppendLine($"<img src='https://dms.az/Users/Uploads/Blogs/{blog.Photo}' alt='image' style=\"width: 95%; height: auto;\" />");
            htmlContent.AppendLine($"<h2>{blog.Title}</h2>");
            htmlContent.AppendLine($"<p>{blog.Description}</p>");
            htmlContent.AppendLine("</body>");
            htmlContent.AppendLine("</html>");

            // Create a TextPart with TextFormat.Html
            var htmlBody = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = htmlContent.ToString()
            };

            // Set the email message body
            emailMessage.Body = htmlBody;

            return emailMessage;
        }


        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfiguration.UserName, _emailConfiguration.Password);
                    client.Send(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception or both.
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
