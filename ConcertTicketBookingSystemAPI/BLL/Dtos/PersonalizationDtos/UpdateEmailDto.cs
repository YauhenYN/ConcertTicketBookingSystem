using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.PersonalizationDtos
{
    public record UpdateEmailDto
    {
        [EmailAddress]
        [Required]
        public string NewEmail { get; init; }
    }
}
