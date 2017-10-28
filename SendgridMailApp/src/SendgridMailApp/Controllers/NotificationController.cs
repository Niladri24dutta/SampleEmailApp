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
        [HttpPost(Name ="SendEmail")]
        public async Task Post([FromBody]string value)
        {
            var apiKey = _configuration.GetSection("SENDGRID_API_KEY").Value;
            //var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("noreply@example.com", "Sendgrid user");
            var subject = "Hello world email from Sendgrid ";

            var to = new EmailAddress("tests@mail.com", "Example User");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }

        
    }
}
