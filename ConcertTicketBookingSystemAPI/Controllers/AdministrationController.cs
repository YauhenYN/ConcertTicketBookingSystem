using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ConcertTicketBookingSystemAPI.Models;
using Microsoft.EntityFrameworkCore;
using ConcertTicketBookingSystemAPI.Helpers;
using ConcertTicketBookingSystemAPI.Dtos.AdministrationDtos;

namespace ConcertTicketBookingSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
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
        public async Task<ActionResult> AddAdminAsync(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (user != null)
            {
                if (!user.IsAdmin)
                {
                    user.IsAdmin = true;
                    await _context.AddActionAsync(Guid.Parse(HttpContext.User.Identity.Name), "Gave user with id = " + id + " admin rights");
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
                else return Conflict();
            }
            else return NotFound();
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> RemoveAdminAsync(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (user != null)
            {
                if (user.IsAdmin)
                {
                    user.IsAdmin = false;
                    await _context.AddActionAsync(Guid.Parse(HttpContext.User.Identity.Name), "Took admin rights from user with id = " + id);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
                else return Conflict();
            }
            else return NotFound();
        }
        [HttpGet]
        [Route("Users/Many/Brief")]
        public async Task<ActionResult<ConcertSelectorDto>> GetManyUsersBriefInfoAsync([FromQuery] ConcertSelectParametersDto dto)
        {
            IQueryable<User> users = _context.Users;
            if (dto.ByIsAdmin != null) users = users.Where(u => dto.ByIsAdmin == u.IsAdmin);
            if (dto.ByUserName != null) users = users.Where(u => u.Name.ToLower().Contains(dto.ByUserName.ToLower()));
            var usersCount = users.Count();
            if (usersCount > 0)
            {
                ConcertSelectorDto selectorDto = new ConcertSelectorDto()
                {
                    PageCount = (usersCount / dto.NeededCount) + 1,
                    CurrentPage = dto.PageNumber,
                    Users = await users.Skip(dto.PageNumber * dto.NeededCount).Take(dto.NeededCount).ToDtosAsync()
                };
                return selectorDto;
            }
            return NotFound();
        }
    }
}
