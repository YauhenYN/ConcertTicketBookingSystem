using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.ActionsDtos
{
    public record AddActionDto
    {
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 5)]
        public string Description { get; init; }
    }
}
