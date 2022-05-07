using System.Threading.Tasks;

namespace PayPal
{
    public interface IPaymentService<Response>
    {
        public Task<Response> CreateOrderAsync(string currencyCode, decimal amount, string description);
        public Task<Response> CaptureOrderAsync(string orderId);
    }
}
