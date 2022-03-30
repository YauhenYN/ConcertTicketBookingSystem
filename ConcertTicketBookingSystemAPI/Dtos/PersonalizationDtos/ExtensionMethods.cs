using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertTicketBookingSystemAPI.Models;

namespace ConcertTicketBookingSystemAPI.Dtos.PersonalizationDtos
{
    public static class ExtensionMethods
    {
        public static UserInfoDto ToUserInfoDto(this User user) => new UserInfoDto()
        {
            BirthDate = user.BirthDate,
            CookieConfirmationFlag = user.CookieConfirmationFlag,
            Email = user.Email,
            IsAdmin = user.IsAdmin,
            Name = user.Name,
            PromoCodeId = user.PromoCodeId,
            UserId = user.UserId
        };
    }
}
