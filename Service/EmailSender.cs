using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyGroup.Service
{
    public class EmailSender: IEmailSender
    {
        private readonly EmailConfiguration configuration;

        public EmailSender(IOptions<EmailConfiguration> options)
        {
            configuration = options.Value;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return DefaultExecute(configuration.SendGridKey, email, subject, htmlMessage);
        }

        public async Task DefaultExecute(string ApiKey, string email, string Subject, string message)
        {
            string apiKey = ApiKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("okesolajibola@gmail.com", "StudyGroup");
            var subject = Subject;
            var to = new EmailAddress(email, null);
            var plainTextContent = message;
            var htmlContent = message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

        }

        //public async Task ManyExecute(string ApiKey, string email, string Subject, string message)
        //{
        //    string apiKey = ApiKey;
        //    var client = new SendGridClient(apiKey);
        //    var from = new EmailAddress("okesolajibola@gmail.com", "StudyGroup");
        //    var subject = Subject;
        //    var to = new EmailAddress(email, null);
        //    var plainTextContent = message;
        //    var htmlContent = message;
        //    var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, to, subject, plainTextContent, htmlContent);
        //    var response = await client.SendEmailAsync(msg);

        //}
    }
}
