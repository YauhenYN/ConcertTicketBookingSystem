using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.PromoCodesDtos
{
    public record AddPromoCodeDto : IValidatableObject
    {
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string UniqueCode { get; init; }
        [Required]
        public decimal Discount { get; init; }
        [Required]
        public bool IsActiveFlag { get; init; }
        [Required]
        public int OnCount { get; init; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            int maxSum = 500;
            var list = new List<ValidationResult>();
            if (Discount * OnCount > maxSum) list.Add(new ValidationResult($"Сумма больше {maxSum}$"));
            else if (Discount * OnCount < 1) list.Add(new ValidationResult("Сумма меньше 1$"));
            return list;
        }
    }
}
