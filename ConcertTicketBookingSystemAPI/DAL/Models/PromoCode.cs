using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class PromoCode
    {
        [Required]
        public Guid PromoCodeId { get; set; }
        [StringLength(20, MinimumLength = 3)]
        [Required]
        public string Code { get; set; }
        [Required]
        [Column(TypeName = "smallmoney")]
        public decimal Discount { get; set; }
        [Required]
        public int TotalCount { get; set; }
        [Required]
        public int LeftCount { get; set; }
        [Required]
        public bool IsActiveFlag { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
