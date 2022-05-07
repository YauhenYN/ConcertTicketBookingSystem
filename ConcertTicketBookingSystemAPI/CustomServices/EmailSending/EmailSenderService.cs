using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace EmailSender
{
    public class EmailSenderService : IDisposable, IEmailSending
    {
        private readonly SmtpClient _client;
        private string _email;
        private string _password;
        private string _name;
        private readonly string _host;
        private int _port;

        public EmailSenderService(string host, int port, string name)
        {
            _name = name;
            _host = host;
            _port = port;
            _client = new SmtpClient();
        }
        public EmailSenderService ConnectAndAuthenticate(string email, string password)
        {
            _email = email;
            _password = password;
            _client.Connect(_host, _port, true);
            _client.Authenticate(email, password);
            return this;
        }
        private MimeMessage GetMimeMessage(string topic, string toEmail, string htmlMessage)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_name, _email));
            emailMessage.To.Add(new MailboxAddress("", toEmail));
            emailMessage.Subject = topic;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = htmlMessage
            };
            return emailMessage;
        }
        private void ConnectAndAuthenticate()
        {
            if (!_client.IsConnected) _client.Connect(_host, _port, true);
            if (!_client.IsAuthenticated) _client.Authenticate(_email, _password);
        }
        public async Task<string> SendHtmlAsync(string topic, string toEmail, string htmlMessage)
        {
            var emailMessage = GetMimeMessage(topic, toEmail, htmlMessage);
            ConnectAndAuthenticate();
            return await _client.SendAsync(emailMessage);
        }
        public string SendHtml(string topic, string toEmail, string htmlMessage, int maxSendAttemptsCount)
        {
            string lastResult = "";
            var emailMessage = GetMimeMessage(topic, toEmail, htmlMessage);
            for (int attempt = 0; attempt < maxSendAttemptsCount; attempt++)
            {
                try
                {
                    ConnectAndAuthenticate();
                    lastResult = _client.Send(emailMessage);
                    break;
                }
                catch { if (attempt > maxSendAttemptsCount - 2) throw; }
            }
            return lastResult;
        }
        public void Dispose()
        {
            _client.Disconnect(true);
            _client.Dispose();
        }
    }
}