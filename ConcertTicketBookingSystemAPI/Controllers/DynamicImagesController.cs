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
    public class DynamicImagesController : ControllerBase
    {
        private readonly ILogger<DynamicImagesController> _logger;

        public DynamicImagesController(ILogger<DynamicImagesController> logger)
        {
            _logger = logger;
        }
    }
}
