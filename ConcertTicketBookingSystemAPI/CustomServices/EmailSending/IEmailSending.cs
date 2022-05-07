using System.Threading.Tasks;

namespace EmailSender
{
    public interface IEmailSending
    {
        public Task<string> SendHtmlAsync(string topic, string recipient, string message);
        public string SendHtml(string topic, string recipient, string htmlMessage, int maxSendAttemptsCount);
    }
}
