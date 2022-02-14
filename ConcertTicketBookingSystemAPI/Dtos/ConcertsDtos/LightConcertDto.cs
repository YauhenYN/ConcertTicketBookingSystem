using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Dtos.ConcertsDtos
{
    public record LightConcertDto
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
        public int LeftCount { get; init; }
        [Required]
        public string Performer { get; init; }
        [Required]
        public DateTime ConcertDate { get; init; }
        [Required]
        public string ConcertType { get; init; }
    }
}
