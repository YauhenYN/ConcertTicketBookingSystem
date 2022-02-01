using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Models
{
    [Table("OpenAirConcerts")]
    public class OpenAirConcert : Concert 
    {
        [Required]
        public string Route { get; set; }
        [Required]
        public string HeadLiner { get; set; }
    }
}
