using System;
using System.Linq;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BLL.Dtos.ConcertsDtos
{
    public static class ExtensionMethods
    {
        public static ConcertDto ToDto(this ClassicConcert concert) => new ConcertDto()
        {
            ClassicConcertInfo = new ClassicConcertDto()
            {
                Compositor = concert.Compositor,
                ConcertName = concert.ConcertName,
                VoiceType = concert.VoiceType
            },
            ConcertId = concert.ConcertId,
            ConcertType = ConcertType.ClassicConcert,
            Cost = concert.Cost,
            CreationTime = concert.CreationTime,
            ConcertDate = concert.ConcertDate,
            IsActiveFlag = concert.IsActiveFlag,
            Latitude = concert.Latitude,
            Longitude = concert.Longitude,
            LeftTicketsCount = concert.LeftCount,
            Performer = concert.Performer,
            TotalCount = concert.TotalCount,
            ImageIds = concert.AdditionalImages == null ? new int[0] : concert.AdditionalImages.Select(i => i.ImageId).ToArray(),
            ImageId = concert.ImageId
        };
        public static ConcertDto ToDto(this OpenAirConcert concert) => new ConcertDto()
        {
            OpenAirConcertInfo = new OpenAirConcertDto()
            {
                HeadLiner = concert.HeadLiner,
                Route = concert.Route
            },
            ConcertId = concert.ConcertId,
            ConcertType = ConcertType.OpenAirConcert,
            Cost = concert.Cost,
            CreationTime = concert.CreationTime,
            ConcertDate = concert.ConcertDate,
            IsActiveFlag = concert.IsActiveFlag,
            Latitude = concert.Latitude,
            Longitude = concert.Longitude,
            LeftTicketsCount = concert.LeftCount,
            Performer = concert.Performer,
            TotalCount = concert.TotalCount,
            ImageIds = concert.AdditionalImages == null ? new int[0] : concert.AdditionalImages.Select(i => i.ImageId).ToArray(),
            ImageId = concert.ImageId
        };
        public static ConcertDto ToDto(this PartyConcert concert) => new ConcertDto()
        {
            PartyConcertInfo = new PartyConcertDto() { Censure = concert.Censure },
            ConcertId = concert.ConcertId,
            ConcertType = ConcertType.PartyConcert,
            Cost = concert.Cost,
            CreationTime = concert.CreationTime,
            ConcertDate = concert.ConcertDate,
            IsActiveFlag = concert.IsActiveFlag,
            Latitude = concert.Latitude,
            Longitude = concert.Longitude,
            LeftTicketsCount = concert.LeftCount,
            Performer = concert.Performer,
            TotalCount = concert.TotalCount,
            ImageIds = concert.AdditionalImages == null ? new int[0] : concert.AdditionalImages.Select(i => i.ImageId).ToArray(),
            ImageId = concert.ImageId
        };
        public static async Task<LightConcertDto[]> ToDtosAsync(this IQueryable<Concert> concerts) => (await concerts.ToArrayAsync()).Select(c =>
        new LightConcertDto()
        {
            ConcertId = c.ConcertId,
            ConcertType = c.GetType() == typeof(ClassicConcert) ? ConcertType.ClassicConcert : c.GetType() == typeof(OpenAirConcert) ? ConcertType.OpenAirConcert : ConcertType.PartyConcert,
            Cost = c.Cost,
            ConcertDate = c.ConcertDate,
            IsActiveFlag = c.IsActiveFlag,
            LeftCount = c.LeftCount,
            Performer = c.Performer,
            ImageId = c.ImageId,
            Longitude = c.Longitude,
            Latitude = c.Latitude
        }).ToArray();
        public static ClassicConcert ToClassicConcert(this AddConcertDto dto, Guid userId, int ImageId) => new ClassicConcert()
        {
            Compositor = dto.ClassicConcertInfo.Compositor,
            ConcertDate = dto.ConcertDate,
            ConcertName = dto.ClassicConcertInfo.ConcertName,
            Cost = dto.Cost,
            IsActiveFlag = dto.IsActiveFlag,
            Latitude = dto.Latitude,
            LeftCount = dto.TotalCount,
            Longitude = dto.Longitude,
            Performer = dto.Performer,
            ImageId = ImageId,
            TotalCount = dto.TotalCount,
            UserId = userId,
            VoiceType = dto.ClassicConcertInfo.VoiceType
        };
        public static OpenAirConcert ToOpenAirConcert(this AddConcertDto dto, Guid userId, int ImageId) => new OpenAirConcert()
        {
            ConcertDate = dto.ConcertDate,
            Cost = dto.Cost,
            IsActiveFlag = dto.IsActiveFlag,
            Latitude = dto.Latitude,
            LeftCount = dto.TotalCount,
            Longitude = dto.Longitude,
            Performer = dto.Performer,
            ImageId = ImageId,
            TotalCount = dto.TotalCount,
            UserId = userId,
            HeadLiner = dto.OpenAirConcertInfo.HeadLiner,
            Route = dto.OpenAirConcertInfo.Route
        };
        public static PartyConcert ToPartyConcert(this AddConcertDto dto, Guid userId, int ImageId) => new PartyConcert()
        {
            ConcertDate = dto.ConcertDate,
            Cost = dto.Cost,
            IsActiveFlag = dto.IsActiveFlag,
            Latitude = dto.Latitude,
            LeftCount = dto.TotalCount,
            Longitude = dto.Longitude,
            Performer = dto.Performer,
            ImageId = ImageId,
            TotalCount = dto.TotalCount,
            UserId = userId,
            Censure = dto.PartyConcertInfo.Censure
        };
        public static Ticket ToTicket(this BuyTicketDto dto, Guid userId, int concertId, Guid? promoCodeId) => new Ticket()
        {
            ConcertId = concertId,
            Count = dto.Count,
            IsMarkedFlag = false,
            TicketId = Guid.NewGuid(),
            UserId = userId,
            PromoCodeId = promoCodeId
        };
    }
}
