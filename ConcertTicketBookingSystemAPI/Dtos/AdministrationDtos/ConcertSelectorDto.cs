using ConcertTicketBookingSystemAPI.Dtos.TicketsDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Dtos.AdministrationDtos
{
    public record ConcertSelectorDto
    {
        [Required]
        public int PageCount { get; init; }
        [Required]
        public int CurrentPage { get; init; }
        [Required]
        public BriefCommonUserInfoDto[] Users { get; init; }
    }
}
