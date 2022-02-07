using System;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using PayPalHttp;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ConcertTicketBookingSystemAPI.CustomServices
{
    public class PayPalPayment
    {
        private readonly PayPalHttpClient _client;
        private readonly PayPalSetup _setup;
        public PayPalPayment(PayPalSetup setup)
        {
            PayPalEnvironment environment = null;
            if (setup.Environment == "live") environment = new LiveEnvironment(setup.ClientId, setup.Secret);
            else if (setup.Environment == "sandbox") environment = new SandboxEnvironment(setup.ClientId, setup.Secret);
            _setup = setup;
            _client = new PayPalHttpClient(environment);
        }
        public async Task<HttpResponse> CreateOrderAsync(string currencyCode, decimal amount, string description)
        {
            var order = new OrderRequest()
            {
                CheckoutPaymentIntent = "CAPTURE",
                PurchaseUnits = new List<PurchaseUnitRequest>()
                    {
                        new PurchaseUnitRequest()
                        {
                            Description = description,
                            AmountWithBreakdown = new AmountWithBreakdown()
                            {
                                CurrencyCode = currencyCode,
                                Value = amount.ToString()
                            }
                        }
                    },
                ApplicationContext = new ApplicationContext()
                {
                    ReturnUrl = _setup.ReturnUrl,
                    CancelUrl = _setup.CancelUrl
                }
            };
            var request = new OrdersCreateRequest();
            request.Prefer("return=representation");
            request.RequestBody(order);
            return await _client.Execute(request);
        }
        public async Task<HttpResponse> CaptureOrderAsync(string orderId)
        {
            var request = new OrdersCaptureRequest(orderId);
            request.RequestBody(new OrderActionRequest());
            return await _client.Execute(request);
        }
    }
}
