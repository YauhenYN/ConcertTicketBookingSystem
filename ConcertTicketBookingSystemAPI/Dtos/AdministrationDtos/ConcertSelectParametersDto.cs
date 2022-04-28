using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Dtos.AdministrationDtos
{
    public record ConcertSelectParametersDto
    {
        [Required]
        public int PageNumber { get; init; }
        [Required]
        public int NeededCount { get; set; }
        public bool? ByIsAdmin { get; init; }
        [StringLength(30, MinimumLength = 3)]
        public string ByUserName { get; init; }
    }
}
