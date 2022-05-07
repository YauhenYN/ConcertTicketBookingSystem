using System;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        public Task<bool> IsAdminAsync(Guid userId);
        public Task<bool> IsExistsAsync(Guid userId);
    }
}
