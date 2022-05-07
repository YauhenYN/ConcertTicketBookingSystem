using BLL.Dtos.UsersDtos;
using System;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserInfoService
    {
        public Task<UserInfoDto> GetUserInfoByRefreshTokenAsync(string refreshToken);
        public Task<UserInfoDto> GetUserInfoByIdAsync(Guid userId);
    }
}
