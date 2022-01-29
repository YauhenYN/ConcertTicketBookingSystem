using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Dtos.PersonalizationDtos
{
    public record UpdateBirthYearDto
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthYear { get; init; }
    }
}
