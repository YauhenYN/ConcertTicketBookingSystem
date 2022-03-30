using ConcertTicketBookingSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Dtos.ImagesDtos
{
    public static class ExtensionMethods
    {
        public static AdditionalImage ToImage(this AddImageDto dto) => new AdditionalImage()
        {
            ConcertId = dto.ConcertId,
            Image = new Image()
            {
                Type = dto.ImageType,
                Source = dto.Image
            }
        };
    }
}
