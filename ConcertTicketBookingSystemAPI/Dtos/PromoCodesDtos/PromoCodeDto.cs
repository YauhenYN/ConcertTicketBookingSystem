using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Dtos.PromoCodesDtos
{
    public record PromoCodeDto
    {
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string UniqueCode { get; init; }
        [Required]
        public decimal Discount { get; init; }
        [Required]
        public bool IsActiveFlag { get; init; }
        public int LeftCount { get; init; }
    }
}
