﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Dtos.AdministrationDtos
{
    public record AddAdminDto
    {
        [Required]
        public Guid Id { get; init; }
    }
}
