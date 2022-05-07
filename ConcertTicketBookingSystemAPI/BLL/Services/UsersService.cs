using BLL.Dtos.UsersDtos;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UsersService : ICommonUsersService, IUserInfoService
    {
        private readonly IUsersRepository _usersRepository;
        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public Task<bool> IsAdminAsync(Guid userId) => _usersRepository.IsAdminAsync(userId);

        public Task<bool> IsExistsAsync(Guid userId) => _usersRepository.IsExistsAsync(userId);
        public async Task<UsersBriefInfoSelectorDto> GetManyUsersBriefInfoAsync(UsersBriefInfoSelectParametersDto dto)
        {
            IQueryable<User> users = _usersRepository.GetQueryable();
            if (dto.ByIsAdmin != null) users = users.Where(u => dto.ByIsAdmin == u.IsAdmin);
            if (dto.ByUserName != null) users = users.Where(u => u.Name.ToLower().Contains(dto.ByUserName.ToLower()));
            var usersCount = users.Count();
            return new UsersBriefInfoSelectorDto()
            {
                PageCount = (usersCount / dto.NeededCount) + 1,
                CurrentPage = dto.PageNumber,
                Users = await users.Skip(dto.PageNumber * dto.NeededCount).Take(dto.NeededCount).ToDtosAsync()
            };
        }

        public async Task<UserInfoDto> GetUserInfoByRefreshTokenAsync(string refreshToken)
        {
            var user = await _usersRepository.GetQueryable().FirstOrDefaultAsync(u => u.RefreshTokenExpiryTime > DateTime.UtcNow && u.RefreshToken == refreshToken);
            return user == null ? null : user.ToUserInfoDto();
        }

        public async Task<UserInfoDto> GetUserInfoByIdAsync(Guid userId)
        {
            var user = await _usersRepository.GetByIdAsync(userId);
            return user == null ? null : user.ToUserInfoDto();
        }
    }
}
