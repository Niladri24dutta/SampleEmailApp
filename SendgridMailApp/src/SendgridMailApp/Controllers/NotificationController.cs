using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Configuration;

namespace SendgridMailApp.Controllers
{
    [Route("api/[controller]")]
    public class NotificationController : Controller
    {
        private readonly IConfiguration _configuration;

        public NotificationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        [Route("SendEmail")]      
        public async Task Post()
        {
            var apiKey = _configuration.GetSection("SENDGRID_API_KEY").Value;
            List<EmailAddress> tos = new List<EmailAddress>
            {
                new EmailAddress("Example1@testmail.com", "Recipient 1"),
                new EmailAddress("Example2@testmail.com", "Recipient 2")
            };
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage();
            var from = new EmailAddress("noreply@example.com", "Sendgrid user");
            var subject = "Hello world email from Sendgrid ";
            var cc = new EmailAddress("Example3@testmail.com", "Recipient CC");
            var htmlContent = "<strong>Hello world in HTML content</strong>";
            msg.SetSubject(subject);
            msg.SetFrom(from);
            msg.AddTos(tos);
            msg.AddCc(cc);
            msg.AddContent(MimeType.Html, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
        
        [Route("SendNotification")]
        public async Task PostMessage()
        {
            var apiKey = _configuration.GetSection("SENDGRID_API_KEY").Value;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("noreply@example.com", "Sendgrid user");
            List<EmailAddress> tos = new List<EmailAddress>
            {
                new EmailAddress("Example1@testmail.com", "Recipient 1"),
                new EmailAddress("Example2@testmail.com", "Recipient 2"),
                new EmailAddress("Example3@testmail.com","Recipient 3")
            };
            
            var subject = "Hello world email from Sendgrid ";
            var displayRecipients = false;
            var htmlContent = "<strong>Hello world in HTML content</strong>";
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, "", htmlContent, displayRecipients);
            var response = await client.SendEmailAsync(msg);
        }


    }
}
