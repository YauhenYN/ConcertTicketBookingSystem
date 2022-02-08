using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace ConcertTicketBookingSystemAPI.CustomServices.EmailSending
{
    public class EmailSenderService : IDisposable
    {
        private readonly SmtpClient _client;
        private readonly string _email;
        private readonly string _password;
        private readonly string _name;
        public EmailSenderService(string host, int port, string name, string email, string password)
        {
            _email = email;
            _password = password;
            _name = name;
            _client = new SmtpClient();
            _client.Connect(host, port, true);
            _client.Authenticate(email, password);
        }
        public async Task<string> SendHtmlAsync(string topic, string toEmail, string htmlMessage)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_name, _email));
            emailMessage.To.Add(new MailboxAddress("", toEmail));
            emailMessage.Subject = topic;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = htmlMessage
            };
            return await _client.SendAsync(emailMessage);
        }
        public void Dispose()
        {
            _client.Disconnect(true);
            _client.Dispose();
        }
    }
}
