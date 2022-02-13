﻿using System;
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
            _client.Connect(_host, _port, true);
            _client.Authenticate(email, password);
            return this;
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
