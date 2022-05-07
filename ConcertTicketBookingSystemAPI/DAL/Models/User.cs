using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
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
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public bool CookieConfirmationFlag { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public List<Concert> Concerts { get; set; }
        public List<Action> Actions { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
