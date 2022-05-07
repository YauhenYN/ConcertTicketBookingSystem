using DAL.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.ConcertsDtos
{
    public record ClassicConcertDto
    {
        [Required]
        [StringLength(15, MinimumLength = 1)]
        public string VoiceType { get; init; }
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string ConcertName { get; init; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Compositor { get; init; }

        public static explicit operator ClassicConcertDto(Concert v)
        {
            throw new NotImplementedException();
        }
    }
}
