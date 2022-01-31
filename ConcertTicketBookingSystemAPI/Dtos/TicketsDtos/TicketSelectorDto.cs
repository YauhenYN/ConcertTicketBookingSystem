using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Dtos.TicketsDtos
{
    public record TicketSelectorDto
    {
        [Required]
        public int PageCount { get; init; }
        [Required]
        public int CurrentPage { get; init; }
        [Required]
        public TicketDto[] Tickets { get; init; }
    }
}
