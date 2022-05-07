using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IClassicConcertsRepository : IRepository<ClassicConcert>
    {
        public Task AddAsync(ClassicConcert concert);
    }
}
