using IServices;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EmailSender : IEmailSender
    {
        public async Task<Response> Execute(string apiKey, string email, string subject, string message)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("shaven999@gmail.com","STalk"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            msg.SetClickTracking(false, false);

            return await client.SendEmailAsync(msg);
        }

        public async Task<Response> SendEmailAsync(string email, string subject, string message)
        {
            return await Execute("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", email, subject, message);
        }
    }   
}
