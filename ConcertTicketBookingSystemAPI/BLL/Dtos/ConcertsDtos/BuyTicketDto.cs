using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.ConcertsDtos
{
    public record BuyTicketDto
    {
        [Range(1, 5)]
        [Required]
        public int Count { get; init; }
    }
}
