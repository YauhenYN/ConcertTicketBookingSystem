using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.UsersDtos 
{
    public record UserBriefInfoDto
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public bool IsAdmin { get; set; }
        public DateTime? BirthDate { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
