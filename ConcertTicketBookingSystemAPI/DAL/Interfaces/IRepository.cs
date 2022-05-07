using System.Linq;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<T>
    {
        public IQueryable<T> GetQueryable();
        public Task SaveChangesAsync();
    }
}
