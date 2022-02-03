using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertTicketBookingSystemAPI.Dtos.AdministrationDtos;
using Microsoft.AspNetCore.Authorization;

namespace ConcertTicketBookingSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "admin")]
    public class AdministrationController : ControllerBase
    {
        private readonly ILogger<AdministrationController> _logger;

        public AdministrationController(ILogger<AdministrationController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> AddAdminAsync(AddAdminDto dto)
        {
            return Ok();
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> RemoveAdminAsync(RemoveAdminDto dto)
        {
            return Ok();
        }
    }
}
