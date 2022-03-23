using ConcertTicketBookingSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Helpers
{
    public static class ActionsHelper
    {
        public static async Task AddActionAsync(this ApplicationContext context, Guid userId, string description)
        {
            await context.Actions.AddAsync(new Models.Action()
            {
                Date = DateTime.Now,
                UserId = userId,
                Description = description
            });
        }
    }
}
