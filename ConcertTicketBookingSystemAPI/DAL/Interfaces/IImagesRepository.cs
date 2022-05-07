using DAL.Models;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IImagesRepository : IRepository<Image>
    {
        public Task<int> AddImageAsync(string source, string type);
        public Task<Image> GetImageByIdAsync(int imageId);
    }
}
