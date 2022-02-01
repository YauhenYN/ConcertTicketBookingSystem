using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Models
{
    public abstract class Concert
    {
        [Required]
        public int ConcertId { get; set; }
        [Required]
        public bool IsActiveFlag { get; set; }
        [Column("ImageId")]
        [Required]
        public Guid PreImageId { get; set; }
        public Image PreImage { get; set; }
        [Required]
        public int Cost { get; set; }
        [Required]
        public string Performer { get; set; }
        [Required]
        public DateTime ConcertDate { get; set; }
        [Required]
        public double Latitude { get; init; }
        [Required]
        public double Longitude { get; init; }
        [Required]
        public int CreatorId { get; set; }
        public User Creator { get; set; }
        [Required]
        public int TotalCount { get; set; }
        [Required]
        public int LeftCount { get; set; }
        [Required]
        public DateTime CreationTime { get; set; }
        public List<Image> Images { get; set; }
    }
}
