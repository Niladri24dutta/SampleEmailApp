using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;



namespace SendgridMailApp.Controllers
{
    [Route("api/[controller]")]
    public class NotificationController : Controller
    {

        // POST api/values
        [HttpPost(Name ="SendEmail")]
        public void Post([FromBody]string value)
        {
        }

        
    }
}
