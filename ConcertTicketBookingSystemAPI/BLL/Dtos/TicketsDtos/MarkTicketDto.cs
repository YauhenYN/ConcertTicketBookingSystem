using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.TicketsDtos
{
    public record MarkTicketDto
    {
        [Required]
        public Guid TicketId { get; init; }
    }
}
