using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.AuthenticationDtos
{
    public record TokensResponse
    {
        [Required]
        public string AccessToken { get; init; }
        [Required]
        public DateTime ExpirationTime { get; init; }
        [Required]
        public string RefreshToken { get; init; }
        [Required]
        public DateTime RefreshExpirationTime { get; init; }
        public QueryString ToQueryString()
        {
            return QueryString.Create("AccessToken", AccessToken)
                .Add("RefreshToken", RefreshToken);
        }
    }
}
