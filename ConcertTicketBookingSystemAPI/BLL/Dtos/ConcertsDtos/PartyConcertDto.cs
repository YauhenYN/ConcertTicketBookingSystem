using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.ConcertsDtos
{
    public record PartyConcertDto
    {
        [Required]
        [Range(3, 21)]
        public int Censure { get; init; }
    }
}
