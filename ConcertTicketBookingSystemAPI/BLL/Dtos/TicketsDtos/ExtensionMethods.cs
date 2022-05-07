using System.Linq;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BLL.Dtos.TicketsDtos
{
    public static class ExtensionMethods
    {
        public static TicketDto ToDto(this Ticket ticket) => new TicketDto()
        {
            IsMarked = ticket.IsMarkedFlag,
            OnCount = ticket.Count,
            PromoCode = ticket.PromoCodeId,
            TicketId = ticket.TicketId,
            UserId = ticket.UserId,
            ConcertId = ticket.ConcertId,
            ConcertPerformer = ticket.Concert.Performer
        };
        public static async Task<TicketDto[]> ToDtosAsync(this IQueryable<Ticket> tickets) => 
            await tickets.Select(t => t.ToDto()).ToArrayAsync();
    }
}
