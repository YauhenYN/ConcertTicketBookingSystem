using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Dtos.TicketsDtos
{
    public record TicketDto
    {
        [Required]
        public Guid TicketId { get; init; }
        [Required]
        public Guid UserId { get; init; }
        [Required]
        [Range(1, 5)]
        public int OnCount { get; init; }
        [Required]
        public bool IsMarked { get; init; }
        public Guid? PromoCode { get; init; }
    }
}
