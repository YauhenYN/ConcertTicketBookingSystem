using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.PromoCodesDtos
{
    public record DeactivatePromoCodeDto
    {
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string UniqueCode { get; init; }
    }
}
