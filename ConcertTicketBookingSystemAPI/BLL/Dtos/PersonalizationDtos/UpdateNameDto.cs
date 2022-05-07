using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.PersonalizationDtos
{
    public record UpdateNameDto
    {
        [StringLength(30, MinimumLength = 3)]
        [Required]
        public string NewName { get; init; }
    }
}
