using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertTicketBookingSystemAPI.Dtos.ImagesDtos;
using ConcertTicketBookingSystemAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

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
        [Route("{imageId}")]
        public async Task<ActionResult<FileResult>> GetImageAsync(int imageId)
        {
            var image = await _context.Images.FirstOrDefaultAsync(i => i.ImageId == imageId);
            if (image != null)
            {
                return new FileContentResult(image.Source, image.Type);
            }
            else return NotFound();
        }
        [HttpPost]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Token")]
        public async Task<ActionResult> AddAdditionalImageAsync(AddImageDto dto)
        {
            var concert = await _context.Concerts.Include(c => c.AdditionalImages).FirstOrDefaultAsync(c => c.ConcertId == dto.ConcertId);
            if (concert != null && concert.AdditionalImages.Count < 5)
            {
                var image = dto.ToImage();
                await _context.AdditionalImages.AddAsync(image);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetImageAsync", new { imageId = image.ImageId });
            }
            else return Conflict();
        }
    }
}
