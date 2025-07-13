using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace AuthServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok("22");
        }


        [HttpPost]
        [Route("SendMailJsonData")]
        public bool SendMailJsonData([FromBody] DataFromJson dataOrder)
        {
            bool res = false;
            if (IsValidEmail(dataOrder.mail))
            {
                res = OrdersManager.Send(dataOrder.mail, "מקורית- הפריטים שהוזמנו", dataOrder.tableItemsOrdered, "", "", "", "", null, "", "", "mekorit.orders@gmail.com", "cfanaqzrhbycdqed");
            }
            if (res)
            {
                res = OrdersManager.Send("mekorit.orders@gmail.com", "MekoritOrderForm_From_Website", JsonConvert.SerializeObject(dataOrder), "", "", "", "", null, "", "", "mekorit.orders@gmail.com", "cfanaqzrhbycdqed");
            }
            return res;
        }

        bool IsValidEmail(string email)
        {
            string regex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
        }

    }
}
