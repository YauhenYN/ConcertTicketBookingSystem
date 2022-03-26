using ConcertTicketBookingSystemAPI.CustomServices.ConfirmationService;
using ConcertTicketBookingSystemAPI.CustomServices.EmailSending;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.CustomServices
{
    public static class ExtensionMethods
    {
        public static void AddGuidConfirmationService(this IServiceCollection collection, int expirationSpan, int timerPeriod)
        {
            collection.AddSingleton<IConfirmationService<Guid, DbContext>, GuidConfirmationService<DbContext>>(provider => new GuidConfirmationService<DbContext>(expirationSpan, timerPeriod));
        }
        public static void AddEmailSenderService(this IServiceCollection collection, string host, int port, string name, string email, string password)
        {
            collection.AddSingleton<EmailSenderService>(provider => new EmailSenderService(host, port, name).ConnectAndAuthenticate(email, password));
        }
    }
}
