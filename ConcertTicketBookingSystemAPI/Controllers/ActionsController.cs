using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertTicketBookingSystemAPI.Dtos.ActionsDtos;
using ConcertTicketBookingSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcertTicketBookingSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public async Task<ActionResult<ActionDto[]>> GetManyAsync(GetManyActionsDto dto)
        {
            var actions = _context.Actions.Where(action => action.UserId == dto.UserId);
            if (actions.Count() > 0) return await actions.ToDtosAsync();
            else return NotFound();
        }
        [HttpPost]
        public async Task<ActionResult> AddAsync(AddActionDto dto)
        {
            var action = dto.ToAction(id);
            _context.Actions.Add(action);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetManyAsync", new GetManyActionsDto() { UserId = action.UserId});
        }
    }
}
