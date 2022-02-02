using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ConcertTicketBookingSystemAPI.Controllers
{
    public class EmailConfirmationController : ControllerBase
    {
        private readonly ILogger<PersonalizationController> _logger;

        public EmailConfirmationController(ILogger<PersonalizationController> logger)
        {
            _logger = logger;
        }
    }
}
