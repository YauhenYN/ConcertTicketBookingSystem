using System;
using System.ComponentModel.DataAnnotations;
using BLL.Dtos.CustomValidationAttributes;

namespace BLL.Dtos.PersonalizationDtos
{
    public record UpdateBirthYearDto
    {
        [Required]
        [DataType(DataType.Date)]
        [BirthDate(1900)]
        public DateTime BirthYear { get; init; }
    }
}
