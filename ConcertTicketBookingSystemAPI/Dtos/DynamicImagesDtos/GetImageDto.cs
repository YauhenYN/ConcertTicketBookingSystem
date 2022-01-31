using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Dtos.DynamicImagesDtos
{
    public class GetImageDto
    {
        [Required]
        public Guid ImageId { get; init; }
    }
}
