using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Dtos.ConcertsDtos
{
    public record ConcertSelectParametersDto
    {
        [Required]
        public int NextPage { get; init; }
        [Required]
        public int NeededCount { get; init; }
        public ConcertType ByConcertType { get; init; }
        public string ByPerformer { get; init; }
        public decimal UntilPrice { get; init; }
        public decimal FromPrice { get; init; }
    }
}
