using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ConcertTicketBookingSystemAPI.CustomValidationAttributes;

namespace ConcertTicketBookingSystemAPI.Dtos.PersonalizationDtos
{
    public record UpdateBirthYearDto
    {
        [Required]
        [DataType(DataType.Date)]
        [BirthDate(1900)]
        public DateTime BirthYear { get; init; }
    }
}
