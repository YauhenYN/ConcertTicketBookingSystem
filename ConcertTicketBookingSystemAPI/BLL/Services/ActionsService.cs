using BLL.Dtos.ActionsDtos;
using BLL.Interfaces;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ActionsService : IActionsService
    {
        private int _maxSelectCount;
        private readonly IActionsRepository _actionsRepository;
        public ActionsService(IActionsRepository actionsRepository, int maxSelectCount = 500)
        {
            _maxSelectCount = maxSelectCount;
            _actionsRepository = actionsRepository;
        }

        public Task<IEnumerable<ActionDto>> GetUserActionsAsync(Guid userId)
        {
            var actions = _actionsRepository.GetQueryable();
            return actions.Where(action => action.UserId == userId).Take(_maxSelectCount).ToDtosAsync();
        }
    }
}
