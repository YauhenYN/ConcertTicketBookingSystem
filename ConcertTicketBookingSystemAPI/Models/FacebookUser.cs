using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Models
{
    [Table("FacebookUsers")]
    public class FacebookUser : User
    {
        [Required]
        public int FacebookId { get; set; }
    }
}
