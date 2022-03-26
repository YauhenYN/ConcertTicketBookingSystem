﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Dtos.PromoCodesDtos
{
    public record GetManyPromoCodesDto
    {
        [Required]
        public bool IsActiveFlag { get; init; }
        [Required]
        public int Count { get; init; }
    }
}
