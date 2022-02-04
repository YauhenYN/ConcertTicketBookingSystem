using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ConcertTicketBookingSystemAPI.Dtos.PersonalizationDtos
{
    public record BuyTicketDto
    {
        [Range(1, 5)]
        [Required]
        public int Count { get; init; }
        [Required]
        public int ConcertId { get; init; }
        public Guid? PromoCodeId { get; init; }
    }
}
