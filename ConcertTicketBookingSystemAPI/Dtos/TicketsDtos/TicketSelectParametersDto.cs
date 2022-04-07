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
        public Guid? ByUserId { get; init; }
        public int? ByConcertId { get; init; }
        [Required]
        public int NeededCount { get; set; }
    }
}
