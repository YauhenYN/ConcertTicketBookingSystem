using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertTicketBookingSystemAPI.Dtos.ActionsDtos;
using ConcertTicketBookingSystemAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ConcertTicketBookingSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ActionsController : ControllerBase
    {
        private readonly ILogger<ActionsController> _logger;
        private readonly ApplicationContext _context;

        public ActionsController(ILogger<ActionsController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        [Route("many")]
        public async Task<ActionResult<ActionDto[]>> GetManyAsync()
        {
            var actions = _context.Actions.Where(action => action.UserId == Guid.Parse(HttpContext.User.Identity.Name));
            if (actions.Count() > 0) return await actions.ToDtosAsync();
            else return NotFound();
        }
    }
}
