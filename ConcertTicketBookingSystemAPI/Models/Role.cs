using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Models
{
    public class Role
    {
        [Required]
        public int RoleId { get; set; }
        //Unique
        [StringLength(10, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }
    }
}
