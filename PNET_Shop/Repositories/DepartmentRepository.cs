using Microsoft.EntityFrameworkCore;
using PNET_Shop.Data;
using PNET_Shop.Models;

namespace PNET_Shop.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ShopDbContext _context;

        public DepartmentRepository(ShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> GetAllAsync(string? searchString = null)
        {
            var query = _context.Departments.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(d =>
                    d.Name.Contains(searchString) ||
                    (d.Info != null && d.Info.Contains(searchString)));
            }

            return await query.OrderBy(d => d.DeptId).ToListAsync();
        }

        public async Task<Department?> GetByIdAsync(int id)
        {
            return await _context.Departments.FindAsync(id);
        }

        public async Task AddAsync(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Department department)
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Departments.AnyAsync(d => d.DeptId == id);
        }

        public async Task<bool> HasGoodsAsync(int departmentId)
        {
            return await _context.Goods.AnyAsync(g => g.DeptId == departmentId);
        }
    }
}
