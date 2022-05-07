using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.UsersDtos
{
    public record UsersBriefInfoSelectorDto
    {
        [Required]
        public int PageCount { get; init; }
        [Required]
        public int CurrentPage { get; init; }
        [Required]
        public UserBriefInfoDto[] Users { get; init; }
    }
}
