using PNET_Shop.Models;

namespace PNET_Shop.Repositories
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAllAsync(string? searchString = null);
        Task<Department?> GetByIdAsync(int id);
        Task AddAsync(Department department);
        Task UpdateAsync(Department department);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> HasGoodsAsync(int departmentId);
    }
}
