using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Options;
using BLL.Configurations;
using BLL.Interfaces;
using System.Threading.Tasks;

namespace PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailConfirmationController : CustomControllerBase
    {
        private readonly BaseLinksConf _baseLinksConf;
        private readonly IEmailConfirmationService _emailConfirmationService;
        public EmailConfirmationController(IOptions<BaseLinksConf> options, IEmailConfirmationService emailConfirmationService)
        {
            _baseLinksConf = options.Value;
            _emailConfirmationService = emailConfirmationService;
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<RedirectResult> ConfirmAsync(Guid confirmationCode)
        {
            await _emailConfirmationService.ConfirmAsync(confirmationCode, UserId);
            return RedirectPermanent(_baseLinksConf.FrontUrl);
        }
    }
}
