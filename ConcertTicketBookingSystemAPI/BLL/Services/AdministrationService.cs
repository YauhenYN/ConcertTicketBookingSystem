using BLL.Interfaces;
using DAL.Interfaces;
using System;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AdministrationService : IAdministrationService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IActionsRepository _actionsRepository;
        public AdministrationService(IUsersRepository usersRepository, IActionsRepository actionsRepository)
        {
            _usersRepository = usersRepository;
            _actionsRepository = actionsRepository;
        }

        public async Task GiveAdminRightsAsync(Guid userId, Guid currentUserId)
        {
            var user = await _usersRepository.GetByIdAsync(userId);
            user.IsAdmin = true;
            await _usersRepository.SaveChangesAsync();
            await _actionsRepository.AddActionAsync(currentUserId, "Gave user with id = " + userId + " admin rights");
        }

        public async Task TakeAdminRightsAsync(Guid userId, Guid currentUserId)
        {
            var user = await _usersRepository.GetByIdAsync(userId);
            user.IsAdmin = false;
            await _usersRepository.SaveChangesAsync();
            await _actionsRepository.AddActionAsync(currentUserId, "Took admin rights from user with id = " + userId);
        }
    }
}
