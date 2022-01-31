using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Dtos.ConcertsDtos
{
    public record ConsertSelectorDto
    {
        [Required]
        public LightConcertDto[] Concerts { get; init; }
        [Required]
        public int PagesCount { get; set; }
        [Required]
        public int CurrentPage { get; set; }
    }
}
