using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Dtos.TicketsDtos
{
    public record TicketSelectParametersDto
    {
        [Required]
        public int PageNumber { get; init; }
        [Required]
        public Guid ByUserId { get; init; }
    }
}
