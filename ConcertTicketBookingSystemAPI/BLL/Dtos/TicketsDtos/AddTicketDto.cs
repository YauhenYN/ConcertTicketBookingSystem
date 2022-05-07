using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.TicketsDtos
{
    public record AddTicketDto
    {
        [Required]
        [Range(1, 5)]
        public int OnCount { get; init; }
        public Guid PromoCode { get; init; }
    }
}
