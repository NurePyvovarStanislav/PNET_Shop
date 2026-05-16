using Microsoft.EntityFrameworkCore;
using PNET_Shop.Data;
using PNET_Shop.Models;

namespace PNET_Shop.Repositories
{
    public class CheckRepository : ICheckRepository
    {
        private readonly ShopDbContext _context;

        public CheckRepository(ShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<Check>> GetAllAsync(string? searchString = null)
        {
            var query = _context.Checks.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(c =>
                    c.CashierName != null && c.CashierName.Contains(searchString));
            }

            return await query
                .OrderByDescending(c => c.CheckDate)
                .ThenByDescending(c => c.CheckNo)
                .ToListAsync();
        }

        public async Task<Check?> GetByIdAsync(int id)
        {
            return await _context.Checks.FindAsync(id);
        }

        public async Task AddAsync(Check check)
        {
            _context.Checks.Add(check);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Check check)
        {
            _context.Checks.Update(check);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var check = await _context.Checks.FindAsync(id);
            if (check != null)
            {
                _context.Checks.Remove(check);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Checks.AnyAsync(c => c.CheckNo == id);
        }

        public async Task<bool> HasSalesAsync(int checkNo)
        {
            return await _context.Sales.AnyAsync(s => s.CheckNo == checkNo);
        }
    }
}
