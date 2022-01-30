using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ConcertTicketBookingSystemAPI.Dtos.PersonalizationDtos
{
    public record ActivatePromocodeDto
    {
        [StringLength(maximumLength: 30, MinimumLength = 5)]
        [Required]
        public string Code { get; init; }
    }
}
