using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Dtos.ConcertsDtos
{
    public record GetConcertDto
    {
        [Required]
        [Range(0, 2)]
        public ConcertType ConcertType { get; init; }
    }
}
