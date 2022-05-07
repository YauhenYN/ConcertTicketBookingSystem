using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IBasicOperationsConcertService
    {
        public Task<bool> IsExistsAsync(int concertId);
    }
}
