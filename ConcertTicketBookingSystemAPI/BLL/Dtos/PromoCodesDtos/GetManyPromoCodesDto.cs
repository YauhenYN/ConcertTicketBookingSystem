using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.PromoCodesDtos
{
    public record GetManyPromoCodesDto
    {
        [Required]
        public bool IsActiveFlag { get; init; }
        [Required]
        public int Count { get; init; }
    }
}
