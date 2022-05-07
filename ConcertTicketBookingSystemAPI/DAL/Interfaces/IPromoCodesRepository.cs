using DAL.Models;
using System;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IPromoCodesRepository : IRepository<PromoCode>
    {
        public Task<PromoCode> GetByIdAsync(Guid id);
        public Task<PromoCode> GetByCodeAsync(string code);
        public Task AddAsync(PromoCode promoCode);
        public Task<bool> IsExistsByCodeAsync(string code);
    }
}
