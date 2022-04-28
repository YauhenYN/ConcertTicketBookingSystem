using ConcertTicketBookingSystemAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Dtos.AdministrationDtos
{
    public static class ExtensionMethods
    {
        public static BriefCommonUserInfoDto ToDto(this User user) => new BriefCommonUserInfoDto()
        {
            BirthDate = user.BirthDate,
            Email = user.Email,
            IsAdmin = user.IsAdmin,
            Name = user.Name,
            UserId = user.UserId
        };
        public static async Task<BriefCommonUserInfoDto[]> ToDtosAsync(this IQueryable<User> users) => await users.Select(t => t.ToDto()).ToArrayAsync();
    }
}
