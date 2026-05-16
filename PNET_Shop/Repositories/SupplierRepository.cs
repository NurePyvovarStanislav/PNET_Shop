using Microsoft.EntityFrameworkCore;
using PNET_Shop.Data;
using PNET_Shop.Models;

namespace PNET_Shop.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ShopDbContext _context;

        public SupplierRepository(ShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<Supplier>> GetAllAsync(string? searchString = null)
        {
            var query = _context.Suppliers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(s =>
                    s.Name.Contains(searchString) ||
                    (s.Phone != null && s.Phone.Contains(searchString)) ||
                    (s.Address != null && s.Address.Contains(searchString)));
            }

            return await query.OrderBy(s => s.SupplierId).ToListAsync();
        }

        public async Task<Supplier?> GetByIdAsync(int id)
        {
            return await _context.Suppliers.FindAsync(id);
        }

        public async Task AddAsync(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Supplier supplier)
        {
            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier != null)
            {
                _context.Suppliers.Remove(supplier);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Suppliers.AnyAsync(s => s.SupplierId == id);
        }

        public async Task<bool> HasGoodsAsync(int supplierId)
        {
            return await _context.Goods.AnyAsync(g => g.SupplierId == supplierId);
        }
    }
}
