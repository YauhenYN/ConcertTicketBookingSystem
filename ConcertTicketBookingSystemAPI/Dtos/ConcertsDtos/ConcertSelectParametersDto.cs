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
        public bool? ByActivity { get; init; }
        public Guid? ByUserId { get; init; }
        [Range(0, 2)]
        public ConcertType? ByConcertType { get; init; }
        [StringLength(50, MinimumLength = 3)]
        public string ByPerformer { get; init; }
        [Range(0.01, 10000)]
        public decimal UntilPrice { get; init; }
        [Range(0.01, 10000)]
        public decimal FromPrice { get; init; }
        [StringLength(20, MinimumLength = 3)]
        public string ByConcertName { get; init; }
        [StringLength(10, MinimumLength = 1)]
        public string ByVoiceType { get; init; }
        [StringLength(30, MinimumLength = 3)]
        public string ByHeadLiner { get; init; }
        public DateTime? DateFrom { get; init; }
        public DateTime? DateUntil { get; init; }
        [StringLength(30, MinimumLength = 3)]
        public string ByCompositor { get; init; }
    }
}
