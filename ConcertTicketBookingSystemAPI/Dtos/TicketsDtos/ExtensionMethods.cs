using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertTicketBookingSystemAPI.Models;

namespace ConcertTicketBookingSystemAPI.Dtos.TicketsDtos
{
    public static class ExtensionMethods
    {
        public static TicketDto ToDto(this Ticket ticket) => new TicketDto() { IsMarked = ticket.IsMarkedFlag, OnCount = ticket.Count, PromoCode = ticket.PromoCodeId, TicketId = ticket.TicketId, UserId = ticket.UserId };
        public static TicketDto[] ToDtos(this Ticket[] tickets) => tickets.Select(t => t.ToDto()).ToArray();
        public static Ticket ToTicket(this AddTicketDto dto) => new Ticket() { Count = dto.OnCount, IsMarkedFlag = false, PromoCodeId = dto.PromoCode, TicketId = Guid.NewGuid(), UserId = dto.UserId };
    }
}
