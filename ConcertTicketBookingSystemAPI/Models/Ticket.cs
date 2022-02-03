using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Models
{
    public class Ticket
    {
        [Required]
        public Guid TicketId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; }
        [Required]
        [Range(1, 5)]
        public int Count { get; set; }
        [Required]
        public Guid PromoCodeId { get; set; }
        public PromoCode PromoCode { get; set; }
        [Required]
        public bool IsMarkedFlag { get; set; }
        [Required]
        public int ConcertId { get; set; }
        public Concert Concert { get; set; }
    }
}
