using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class AdditionalImagesRepository : Repository, IAdditionalImagesRepository
    {
        public AdditionalImagesRepository(ApplicationContext context)
            : base(context)
        {
        }

        public async Task<int> AddAdditionalImageAsync(int concertId, string source, string type)
        {
            var additionalImage = new AdditionalImage()
            {
                ConcertId = concertId,
                Image = new Image()
                {
                    Source = Convert.FromBase64String(source),
                    Type = type
                }
            };
            await _context.AdditionalImages.AddAsync(additionalImage);
            await _context.SaveChangesAsync();
            return additionalImage.ConcertId;
        }

        public Task<int> GetAdditionalImagesCountByConcertIdAsync(int concertId)
        {
            return Task.FromResult(_context.Concerts.Include(c => c.AdditionalImages).First(c => c.ConcertId == concertId).AdditionalImages.Count);
        }

        public IQueryable<AdditionalImage> GetQueryable()
        {
            return _context.AdditionalImages;
        }
    }
}
