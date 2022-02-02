using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Dtos.PromoCodesDtos
{
    public record GetManyPromoCodesDto
    {
        [Required]
        public bool IsActive { get; init; }
        [Required]
        public int Count { get; init; }
        public Guid? ById { get; init; }
        public string ByCode { get; init; }
    }
}
