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
        public Guid ByUserId { get; init; }
        [Range(0, 2)]
        public ConcertType? ByConcertType { get; init; }
        [StringLength(50, MinimumLength = 3)]
        public string ByPerformer { get; init; }
        [Range(0.01, 10000)]
        public decimal UntilPrice { get; init; }
        [Range(0.01, 10000)]
        public decimal FromPrice { get; init; }
    }
}
