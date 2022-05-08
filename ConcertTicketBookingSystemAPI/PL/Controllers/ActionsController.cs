using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using BLL.Dtos.ActionsDtos;
using BLL.Interfaces;
using System.Collections.Generic;

namespace PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ActionsController : CustomControllerBase
    {
        private readonly IActionsService _actionsService;
        public ActionsController(IActionsService actionsService)
        {
            _actionsService = actionsService;
        }
        [HttpGet]
        [Route("many")]
        public async Task<ActionResult<IEnumerable<ActionDto>>> GetManyAsync()
        {
            var actions = await _actionsService.GetUserActionsAsync(UserId.Value);
            if (actions.Count() > 0) return actions.ToArray();
            else return NotFound();
        }
    }
}
