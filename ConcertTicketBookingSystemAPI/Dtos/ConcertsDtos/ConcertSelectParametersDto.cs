using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Dtos.ConcertsDtos
{
    public enum ConcertType
    {
        ClassicConcert,
        OpenAirConcert,
        PartyConcert
    }
    public record ConcertSelectParametersDto
    {
        [Required]
        public int NextPage { get; init; }
        public ConcertType ByConcertType { get; init; }
        public string ByName { get; init; }
        public decimal UntilPrice { get; init; }
        public decimal FromPrice { get; init; }
    }
}
