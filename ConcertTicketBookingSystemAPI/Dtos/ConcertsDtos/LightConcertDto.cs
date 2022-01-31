﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Dtos.ConcertsDtos
{
    public record LightConcertDto
    {
        [Required]
        public int ConcertId { get; init; }
        [Required]
        public bool IsActive { get; init; }
        [Required]
        [Url]
        public string PreImageUrl { get; init; }
        [Required]
        public decimal Cost { get; init; }
        [Required]
        public int LeftTicketsCount { get; init; }
        [Required]
        public string Performer { get; init; }
        [Required]
        public DateTime EventTime { get; init; }
        [Required]
        public string ConcertType { get; init; }
    }
}