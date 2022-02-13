using ConcertTicketBookingSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Dtos.ConcertsDtos
{
    public record ClassicConcertDto
    {
        [Required]
        [MaxLength(10)]
        public string VoiceType { get; init; }
        [Required]
        [MaxLength(20)]
        public string ConcertName { get; init; }
        [Required]
        [MaxLength(30)]
        public string Compositor { get; init; }

        public static explicit operator ClassicConcertDto(Concert v)
        {
            throw new NotImplementedException();
        }
    }
}
