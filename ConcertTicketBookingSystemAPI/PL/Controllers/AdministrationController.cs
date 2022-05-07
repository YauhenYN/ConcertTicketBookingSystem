using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using BLL.Interfaces;
using BLL.Dtos.UsersDtos;

namespace PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
    public class AdministrationController : CustomControllerBase
    {
        private readonly IAdministrationService _administrationService;
        private readonly ICommonUsersService _usersService;
        public AdministrationController(IAdministrationService administrationService,
            ICommonUsersService usersService)
        {
            _administrationService = administrationService;
            _usersService = usersService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> AddAdminAsync(Guid id)
        {
            if (await _usersService.IsAdminAsync(id)) return Conflict();
            if (await _usersService.IsExistsAsync(id))
            {
                await _administrationService.GiveAdminRightsAsync(id, UserId);
                return NoContent();
            }
            else return NotFound();
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> RemoveAdminAsync(Guid id)
        {
            if (!await _usersService.IsAdminAsync(id)) return Conflict();
            if (await _usersService.IsExistsAsync(id))
            {
                await _administrationService.TakeAdminRightsAsync(id, UserId);
                return NoContent();
            }
            else return NotFound();
        }
        [HttpGet]
        [Route("Users/Many/Brief")]
        public async Task<ActionResult<UsersBriefInfoSelectorDto>> GetManyUsersBriefInfoAsync([FromQuery] UsersBriefInfoSelectParametersDto dto)
        {
            var usersBriefInfo = await _usersService.GetManyUsersBriefInfoAsync(dto);
            if (usersBriefInfo.Users.Length > 0) return usersBriefInfo;
            else return NotFound();
        }
    }
}
