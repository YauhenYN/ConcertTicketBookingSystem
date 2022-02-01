using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Models
{
    [Table("MicrosoftUsers")]
    public class MicrosoftUser : User 
    {
        [Required]
        public int MicrosoftId { get; set; }
    }
}
