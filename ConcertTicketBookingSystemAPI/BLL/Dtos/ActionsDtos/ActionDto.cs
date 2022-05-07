using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace BLL.Dtos.ActionsDtos
{
    public record ActionDto
    {
        [Required]
        public Guid UserId { get; init; }
        public DateTime CreationTime { get; init; }
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 5)]
        public string Description { get; init; }
    }
}
