using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.PromoCodesDtos
{
    public class GetPromoCodeByCode
    {
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Code { get; init; }
    }
}
