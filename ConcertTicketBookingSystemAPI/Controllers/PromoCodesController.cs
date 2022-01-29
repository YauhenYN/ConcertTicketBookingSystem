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
    public class PromoCodesController : ControllerBase
    {
        private readonly ILogger<PromoCodesController> _logger;

        public PromoCodesController(ILogger<PromoCodesController> logger)
        {
            _logger = logger;
        }
    }
}
