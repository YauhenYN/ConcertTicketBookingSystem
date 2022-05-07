using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PromoCodesRepository : Repository, IPromoCodesRepository
    {
        public PromoCodesRepository(ApplicationContext context)
            :base(context)
        {
        }

        public async Task AddAsync(PromoCode promoCode)
        {
            await _context.PromoCodes.AddAsync(promoCode);
        }

        public Task<PromoCode> GetByCodeAsync(string code)
        {
            return _context.PromoCodes.FirstAsync(p => p.Code == code);
        }

        public Task<PromoCode> GetByIdAsync(Guid id)
        {
            return _context.PromoCodes.FirstAsync(p => p.PromoCodeId == id);
        }

        public IQueryable<PromoCode> GetQueryable()
        {
            return _context.PromoCodes;
        }

        public Task<bool> IsExistsByCodeAsync(string code)
        {
            return _context.PromoCodes.AnyAsync(p => p.Code == code);
        }
    }
}
