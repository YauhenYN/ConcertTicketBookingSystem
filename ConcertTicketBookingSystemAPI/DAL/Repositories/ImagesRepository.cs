using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ImagesRepository : Repository, IImagesRepository
    {
        public ImagesRepository(ApplicationContext context)
            :base(context)
        {
        }

        public async Task<int> AddImageAsync(string source, string type)
        {
            var image = new Image()
            {
                Source = Convert.FromBase64String(source),
                Type = type
            };
            await _context.Images.AddAsync(image);
            await _context.SaveChangesAsync();
            return image.ImageId;
        }

        public Task<Image> GetImageByIdAsync(int imageId)
        {
            return _context.Images.FirstAsync(i => i.ImageId == imageId);
        }

        public IQueryable<Image> GetQueryable()
        {
            return _context.Images;
        }
    }
}
