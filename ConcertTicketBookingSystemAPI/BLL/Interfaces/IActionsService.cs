using BLL.Dtos.ActionsDtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IActionsService
    {
        public Task<IEnumerable<ActionDto>> GetUserActionsAsync(Guid userId);
    }
}
