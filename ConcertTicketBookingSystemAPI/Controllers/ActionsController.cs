using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertTicketBookingSystemAPI.Dtos.ActionsDtos;

namespace ConcertTicketBookingSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActionsController : ControllerBase
    {
        private readonly ILogger<ActionsController> _logger;

        public ActionsController(ILogger<ActionsController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<ActionDto[]>> GetManyAsync(GetManyActionsDto dto)
        {
            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult> AddAsync(AddActionDto dto)
        {
            return Ok();
        }
    }
}
