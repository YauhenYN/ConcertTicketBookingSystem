using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Dtos.ConcertsDtos
{
    public record PartyConcertDto
    {
        [Required]
        [Range(3, 21)]
        public int Censure { get; init; }
    }
}
