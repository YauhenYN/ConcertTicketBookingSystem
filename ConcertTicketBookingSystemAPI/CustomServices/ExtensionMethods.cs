using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.CustomServices
{
    public static class ExtensionMethods
    {
        public static void AddEmailConfirmationService(this IServiceCollection collection, TimeSpan expirationSpan, int timerPeriod)
        {
            collection.AddSingleton<IConfirmationService<Guid>, EmailConfirmationService>(provider => new EmailConfirmationService(expirationSpan, timerPeriod));
        }
    }
}
