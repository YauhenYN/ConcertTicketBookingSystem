using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Models
{
    [Table("ClassicConcerts")]
    public class ClassicConcert : Concert
    {
        [Required]
        public string VoiceType { get; set; }
        [Required]
        public string ConcertName { get; set; }
        [Required]
        public string Compositor { get; set; }
    }
}
