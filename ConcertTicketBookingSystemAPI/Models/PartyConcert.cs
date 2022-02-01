using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Models
{
    [Table("PartyConcerts")]
    public class PartyConcert : Concert
    {
        [Required]
        public int Censure { get; set; }
    }
}
