using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IPartyConcertsRepository : IRepository<PartyConcert>
    {
        public Task AddAsync(PartyConcert concert);
    }
}
