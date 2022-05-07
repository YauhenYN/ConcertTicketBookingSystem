using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.PersonalizationDtos
{
    public record ActivatePromocodeDto
    {
        [StringLength(maximumLength: 30, MinimumLength = 3)]
        [Required]
        public string Code { get; init; }
    }
}
