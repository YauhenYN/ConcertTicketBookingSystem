using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Models;

namespace BLL.Dtos.ActionsDtos
{
    public static class ExtensionMethods
    {
        public static async Task<IEnumerable<ActionDto>> ToDtosAsync(this IQueryable<Action> actions) => await actions.Select(action => new ActionDto()
        {
            CreationTime = action.Date,
            Description = action.Description,
            UserId = action.UserId
        }).ToArrayAsync();
        public static Action ToAction(this AddActionDto dto, System.Guid userId) => new Action()
        {
            Description = dto.Description,
            UserId = userId
        };
    }
}
