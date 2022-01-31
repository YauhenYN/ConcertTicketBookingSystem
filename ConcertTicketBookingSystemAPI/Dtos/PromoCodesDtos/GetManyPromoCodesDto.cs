using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Dtos.PromoCodesDtos
{
    public record GetManyPromoCodesDto
    {
        public bool IsActive { get; init; }
        public int Count { get; init; }
    }
}
