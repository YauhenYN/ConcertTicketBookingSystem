﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Dtos.PersonalizationDtos
{
    public record UserInfoDto
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public bool IsAdmin { get; set; }
        public DateTime? BirthDate { get; set; }
        public Guid? PromoCodeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public bool CookieConfirmationFlag { get; set; }
    }
}