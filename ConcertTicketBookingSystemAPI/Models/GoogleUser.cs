using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Models
{
    [Table("GoogleUsers")]
    public class GoogleUser : User
    {
        [Required]
        public int GoogleId { get; set; }
    }
}
