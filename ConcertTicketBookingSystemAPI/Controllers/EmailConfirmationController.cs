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
using Microsoft.EntityFrameworkCore;
using ConcertTicketBookingSystemAPI.Models;

namespace ConcertTicketBookingSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailConfirmationController : ControllerBase
    {
        private readonly ILogger<PersonalizationController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IConfirmationService<Guid, DbContext> _confirmationService;
        private readonly ApplicationContext _context;

        public EmailConfirmationController(ILogger<PersonalizationController> logger, ApplicationContext context, IConfiguration configuration, IConfirmationService<Guid, DbContext> confirmationService)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _confirmationService = confirmationService;
        }
        [HttpGet]
        [Route("[action]")]
        public RedirectResult Confirm(Guid confirmationCode)
        {
            _confirmationService.Confirm(confirmationCode, _context);
            return RedirectPermanent(_configuration["RedirectUrl"]);
        }
    }
}
