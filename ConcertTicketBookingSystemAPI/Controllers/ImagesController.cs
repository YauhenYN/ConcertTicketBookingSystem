using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertTicketBookingSystemAPI.Dtos.ImagesDtos;
using ConcertTicketBookingSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcertTicketBookingSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly ILogger<ImagesController> _logger;
        private readonly ApplicationContext _context;

        public ImagesController(ILogger<ImagesController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<FileResult>> GetImageAsync(GetImageDto dto)
        {
            var image = await _context.Images.FirstOrDefaultAsync(i => i.ImageId == dto.ImageId);
            if (image != null)
            {
                return new FileContentResult(image.Source, image.Type);
            }
            else return NotFound();
        }
    }
}
