using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertTicketBookingSystemAPI.Dtos.AdministrationDtos;
using Microsoft.AspNetCore.Authorization;
using ConcertTicketBookingSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcertTicketBookingSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "admin", AuthenticationSchemes = "Token")]
    public class AdministrationController : ControllerBase
    {
        private readonly ILogger<AdministrationController> _logger;
        ApplicationContext _context;

        public AdministrationController(ILogger<AdministrationController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> AddAdminAsync(AddAdminDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == dto.Id);
            if (user != null)
            {
                if (!user.IsAdmin)
                {
                    user.IsAdmin = true;
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
                else return Conflict();
            }
            else return NotFound();
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> RemoveAdminAsync(RemoveAdminDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == dto.Id);
            if (user != null)
            {
                if (user.IsAdmin)
                {
                    user.IsAdmin = false;
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
                else return Conflict();
            }
            else return NotFound();
        }
    }
}
