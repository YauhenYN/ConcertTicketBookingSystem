using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConcertsController : ControllerBase
    {
        private readonly ILogger<ConcertsController> _logger;

        public ConcertsController(ILogger<ConcertsController> logger)
        {
            _logger = logger;
        }
    }
}
