using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IAdditionalImagesRepository : IRepository<AdditionalImage>
    {
        public Task<int> AddAdditionalImageAsync(int concertId, string source, string type);
        public Task<int> GetAdditionalImagesCountByConcertIdAsync(int concertId);
    }
}
