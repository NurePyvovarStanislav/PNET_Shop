using Microsoft.EntityFrameworkCore;
using PNET_Shop.Data;
using PNET_Shop.Models;

namespace PNET_Shop.Repositories
{
    public class GoodRepository : IGoodRepository
    {
        private readonly ShopDbContext _context;

        public GoodRepository(ShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<Good>> GetAllAsync(string? searchString = null)
        {
            var query = _context.Goods
                .Include(g => g.Department)
                .Include(g => g.Supplier)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(g =>
                    g.Name.Contains(searchString) ||
                    (g.Producer != null && g.Producer.Contains(searchString)) ||
                    (g.Department != null && g.Department.Name.Contains(searchString)) ||
                    (g.Supplier != null && g.Supplier.Name.Contains(searchString)));
            }

            return await query.ToListAsync();
        }

        public async Task<Good?> GetByIdAsync(int id)
        {
            return await _context.Goods
                .Include(g => g.Department)
                .Include(g => g.Supplier)
                .FirstOrDefaultAsync(g => g.GoodId == id);
        }

        public async Task AddAsync(Good good)
        {
            _context.Goods.Add(good);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Good good)
        {
            _context.Goods.Update(good);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var good = await _context.Goods.FindAsync(id);
            if (good != null)
            {
                _context.Goods.Remove(good);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Goods.AnyAsync(g => g.GoodId == id);
        }
    }
}
