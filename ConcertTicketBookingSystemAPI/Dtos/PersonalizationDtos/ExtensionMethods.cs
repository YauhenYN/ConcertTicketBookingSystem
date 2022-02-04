using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertTicketBookingSystemAPI.Models;

namespace ConcertTicketBookingSystemAPI.Dtos.PersonalizationDtos
{
    public static class ExtensionMethods
    {
        public static Ticket ToTicket(this BuyTicketDto dto, Guid userId) => new Ticket() { ConcertId = dto.ConcertId, Count = dto.Count, IsMarkedFlag = false, TicketId = Guid.NewGuid(), UserId = userId, PromoCodeId = dto.PromoCodeId };
    }
}
