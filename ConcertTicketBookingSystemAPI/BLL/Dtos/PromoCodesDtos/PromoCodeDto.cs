using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.PromoCodesDtos
{
    public record PromoCodeDto
    {
        [Required]
        public Guid PromoCodeId { get; set; }
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string UniqueCode { get; init; }
        [Required]
        public decimal Discount { get; init; }
        [Required]
        public bool IsActiveFlag { get; init; }
        public int LeftCount { get; init; }
    }
}
