using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Dtos.ConcertsDtos
{
    public record OpenAirConcertDto
    {
        [Required]
        [StringLength(maximumLength: 200, MinimumLength = 1)]
        public string Route { get; init; }
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string HeadLiner { get; init; }
    }
}
