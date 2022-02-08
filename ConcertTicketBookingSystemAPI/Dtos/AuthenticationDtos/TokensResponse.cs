using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Dtos.AuthenticationDtos
{
    public record TokensResponse
    {
        [Required]
        public string AccessToken { get; init; }
        [Required]
        public string RefreshToken { get; init; }
        [Required]
        public DateTime ExpirationTime { get; init; }
    }
}
