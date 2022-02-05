using ConcertTicketBookingSystemAPI.CustomServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PayPalCheckoutSdk.Orders;
using PayPalCheckoutSdk.Core;
using PayPalHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PayPalPaymentController : ControllerBase
    {
        private readonly ILogger<ConcertsController> _logger;
        private readonly PayPalPayment _payment;

        public PayPalPaymentController(ILogger<ConcertsController> logger, PayPalPayment payment)
        {
            _logger = logger;
            _payment = payment;
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> Paypalvtwo()
        {
            var orderId = Request.Headers["token"];
            string payerID = Request.Headers["PayerID"];
            //when payerID is null it means order is not approved by the payer.    
            if (string.IsNullOrEmpty(payerID))
            {
                HttpResponse response = await _payment.CreateOrderAsync("USD", 1, "Count: " + 1 + "JUST SOMETHING");
                var statusCode = response.StatusCode;
                Order result = response.Result<Order>();
                string approveUrl = null;
                foreach (LinkDescription link in result.Links)
                {
                    if (link.Rel.Trim().ToLower() == "approve")
                    {
                        approveUrl = link.Href;
                    }
                }
                if (!string.IsNullOrEmpty(approveUrl)) return Redirect(approveUrl);
            }
            else
            {
                //this is where actual transaction is carried out
                HttpResponse response = await _payment.CaptureOrderAsync(orderId);
                var statusCode = response.StatusCode;
                Order result = response.Result<Order>();
                //if (result.Status.Trim().ToUpper() == "COMPLETED") ("Payment Successful. Thank you.");
                return Ok();
            }
            return Conflict();
        }
    }
}
