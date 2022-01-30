﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertTicketBookingSystemAPI.Dtos.PromoCodesDtos;

namespace ConcertTicketBookingSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PromoCodeController : ControllerBase
    {
        private readonly ILogger<PromoCodeController> _logger;

        public PromoCodeController(ILogger<PromoCodeController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddPromoCodeAsync(AddPromoCodeDto dto)
        {
            //If promocode already exists, just change a flag in it on active
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeactivatePromoCodeAsync(DeactivatePromoCodeDto dto)
        {

            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> GetManyAsync(GetManyDto dto)
        {
            return Ok();
        }
    }
}