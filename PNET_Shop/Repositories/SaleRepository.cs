using Microsoft.EntityFrameworkCore;
using PNET_Shop.Data;
using PNET_Shop.Models;

namespace PNET_Shop.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly ShopDbContext _context;

        public SaleRepository(ShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<Sale>> GetAllAsync(string? searchString = null)
        {
            var query = _context.Sales
                .Include(s => s.Good)
                .Include(s => s.Check)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(s =>
                    (s.Good != null && s.Good.Name.Contains(searchString)) ||
                    s.CheckNo.ToString().Contains(searchString));
            }

            return await query
                .OrderByDescending(s => s.SaleId)
                .ToListAsync();
        }

        public async Task<Sale?> GetByIdAsync(int id)
        {
            return await _context.Sales
                .Include(s => s.Good)
                .Include(s => s.Check)
                .FirstOrDefaultAsync(s => s.SaleId == id);
        }

        public async Task AddAsync(Sale sale)
        {
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Sale sale)
        {
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale != null)
            {
                _context.Sales.Remove(sale);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Sales.AnyAsync(s => s.SaleId == id);
        }
    }
}
