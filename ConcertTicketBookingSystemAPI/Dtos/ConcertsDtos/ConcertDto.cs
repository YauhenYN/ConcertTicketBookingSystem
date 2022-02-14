using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Dtos.ConcertsDtos
{
    public record ConcertDto
    {
        [Required]
        public int ConcertId { get; init; }
        [Required]
        public bool IsActiveFlag { get; init; }
        [Required]
        public string PreImageType { get; init; }
        [Required]
        public byte[] PreImage { get; init; }
        [Required]
        public decimal Cost { get; init; }
        [Required]
        public int TotalCount { get; init; }
        [Required]
        public int LeftTicketsCount { get; init; }
        [Required]
        public string Performer { get; init; }
        [Required]
        public DateTime ConcertDate { get; init; }
        [Required]
        public double Latitude { get; init; }
        [Required]
        public double Longitude { get; init; }
        [Required]
        public string ConcertType { get; init; }
        [Required]
        public DateTime CreationTime { get; init; }
        [Range(0, 5)]
        public Guid[] ImageIds { get; init; }
        public ClassicConcertDto ClassicConcertInfo { get; init; }
        public OpenAirConcertDto OpenAirConcertInfo { get; init; }
        public PartyConcertDto PartyConcertInfo { get; init; }
    }
}
