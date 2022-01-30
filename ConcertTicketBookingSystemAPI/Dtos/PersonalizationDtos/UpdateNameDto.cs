using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Dtos.PersonalizationDtos
{
    public record UpdateNameDto
    {
        [StringLength(30, MinimumLength = 3)]
        [Required]
        public string NewName { get; init; }
    }
}
