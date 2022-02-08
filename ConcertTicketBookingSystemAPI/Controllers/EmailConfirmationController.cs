using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertTicketBookingSystemAPI.CustomServices;
using Microsoft.AspNetCore.Authorization;
using ConcertTicketBookingSystemAPI.CustomServices.ConfirmationService;

namespace ConcertTicketBookingSystemAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Token")]
    [ApiController]
    [Route("[controller]")]
    public class EmailConfirmationController : ControllerBase
    {
        private readonly ILogger<PersonalizationController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IConfirmationService<Guid> _confirmationService;

        public EmailConfirmationController(ILogger<PersonalizationController> logger, IConfiguration configuration, IConfirmationService<Guid> confirmationService)
        {
            _logger = logger;
            _configuration = configuration;
            _confirmationService = confirmationService;
        }
        [HttpGet]
        [Route("[action]")]
        public RedirectResult Confirm(Guid confirmationCode)
        {
            _confirmationService.Confirm(confirmationCode);
            return RedirectPermanent(_configuration["RedirectUrl"]);
        }
    }
}
