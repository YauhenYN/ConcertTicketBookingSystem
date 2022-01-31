﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Dtos.ConcertsDtos
{
    public record GetConcertDto
    {
        [Required]
        public int ConcertId { get; init; }
    }
}