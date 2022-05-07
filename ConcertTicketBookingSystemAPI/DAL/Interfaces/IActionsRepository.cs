using System;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IActionsRepository : IRepository<Models.Action>
    {
        public Task AddActionAsync(Guid userId, string description);
    }
}
