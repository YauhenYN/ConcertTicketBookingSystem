﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Dtos.ActionsDtos
{
    public record GetManyActionsDto
    {
        [Required]
        public Guid UserId { get; init; }
    }
}