using BLL.Dtos.UsersDtos;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUsersService
    {
        public Task<UsersBriefInfoSelectorDto> GetManyUsersBriefInfoAsync(UsersBriefInfoSelectParametersDto dto);
    }
}
