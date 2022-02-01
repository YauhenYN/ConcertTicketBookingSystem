﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Models
{
    public class Image
    {
        [Required]
        public Guid ImageId { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public byte[] Source { get; set; }
        [Required]
        public int ConcertId { get; set; }
    }
}
