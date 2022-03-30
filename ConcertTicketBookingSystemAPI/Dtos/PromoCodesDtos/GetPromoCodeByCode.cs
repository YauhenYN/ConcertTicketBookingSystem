using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Dtos.PromoCodesDtos
{
    public class GetPromoCodeByCode
    {
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Code { get; init; }
    }
}
