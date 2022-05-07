using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IConcertsRepository : IRepository<Concert>
    {
        public Task<Concert> GetByIdAsync(int id);
        public Task<Concert> GetByIdIncludingAsync<Y>(int id, Func<Concert, Y> predicate);
        public Task<bool> IsExistsAsync(int id);
    }
}
