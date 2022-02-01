using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Models
{
    public abstract class User
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public bool IsAdmin { get; set; }
        public DateTime? BirthDate { get; set; }
        public Guid? PromoCodeId { get; set; }
        public PromoCode PromoCode { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [Required]
        public bool CookieConfirmationFlag { get; set; }
        public List<Action> Actions { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
