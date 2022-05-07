using BLL.Interfaces;
using ConfirmationService;
using DAL.Interfaces;
using System;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class EmailConfirmationService : IEmailConfirmationService
    {
        private readonly IConfirmationService<Guid, IUsersRepository> _confirmationService;
        private readonly IUsersRepository _usersRepository;
        private readonly IActionsRepository _actionsRepository;
        public EmailConfirmationService(IConfirmationService<Guid, IUsersRepository> confirmationService,
            IUsersRepository usersRepository,
            IActionsRepository actionsRepository)
        {
            _confirmationService = confirmationService;
            _usersRepository = usersRepository;
            _actionsRepository = actionsRepository;
        }

        public async Task ConfirmAsync(Guid confirmationCode, Guid userId)
        {
            _confirmationService.Confirm(confirmationCode, _usersRepository);
            await _actionsRepository.AddActionAsync(userId, "Updated Email");
        }
    }
}
