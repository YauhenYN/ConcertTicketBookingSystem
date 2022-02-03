using ConcertTicketBookingSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Dtos.ImagesDtos
{
    public static class ExtensionMethods
    {
        public static Image ToImage(this AddImageDto dto) => new Image() { ImageId = Guid.NewGuid(), ConcertId = dto.ConcertId, Type = dto.ImageType, Source = dto.Image };
    }
}
