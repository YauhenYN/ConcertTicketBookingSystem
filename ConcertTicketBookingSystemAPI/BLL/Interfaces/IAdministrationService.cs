using System;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAdministrationService
    {
        public Task GiveAdminRightsAsync(Guid userId, Guid currentUserId);
        public Task TakeAdminRightsAsync(Guid userId, Guid currentUserId);
    }
}
