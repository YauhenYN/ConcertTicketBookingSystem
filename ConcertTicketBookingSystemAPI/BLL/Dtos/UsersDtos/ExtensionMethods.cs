using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Dtos.UsersDtos
{
    public static class ExtensionMethods
    {
        public static UserBriefInfoDto ToDto(this User user) => new UserBriefInfoDto()
        {
            BirthDate = user.BirthDate,
            Email = user.Email,
            IsAdmin = user.IsAdmin,
            Name = user.Name,
            UserId = user.UserId
        };
        public static async Task<UserBriefInfoDto[]> ToDtosAsync(this IQueryable<User> users) => await users.Select(t => t.ToDto()).ToArrayAsync();
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
