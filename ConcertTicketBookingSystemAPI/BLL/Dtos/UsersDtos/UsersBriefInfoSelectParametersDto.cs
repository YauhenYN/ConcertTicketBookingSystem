using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.UsersDtos
{
    public record UsersBriefInfoSelectParametersDto
    {
        [Required]
        public int PageNumber { get; init; }
        [Required]
        public int NeededCount { get; set; }
        public bool? ByIsAdmin { get; init; }
        [StringLength(30, MinimumLength = 1)]
        public string ByUserName { get; init; }
    }
}
