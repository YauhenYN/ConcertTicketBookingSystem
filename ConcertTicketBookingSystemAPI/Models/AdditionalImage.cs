using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Models
{
    public class AdditionalImage
    {
        [Required]
        [Key]
        public int ImageId { get; set; }
        public Image Image { get; set; }
        [Required]
        public int ConcertId { get; set; }
    }
}
