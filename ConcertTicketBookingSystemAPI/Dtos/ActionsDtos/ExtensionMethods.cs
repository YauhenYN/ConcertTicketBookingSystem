using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ConcertTicketBookingSystemAPI.Dtos.ActionsDtos
{
    public static class ExtensionMethods
    {
        public static async Task<ActionDto[]> ToDtosAsync(this IQueryable<Models.Action> actions) => await actions.Select(action => new ActionDto()
        {
            CreationTime = action.Date,
            Description = action.Description,
            UserId = action.UserId
        }).ToArrayAsync();
        public static Models.Action ToAction(this AddActionDto dto, Guid userId) => new Models.Action()
        {
            Description = dto.Description,
            UserId = userId
        };
    }
}
