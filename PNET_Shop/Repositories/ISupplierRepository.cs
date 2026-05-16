using PNET_Shop.Models;

namespace PNET_Shop.Repositories
{
    public interface ISupplierRepository
    {
        Task<List<Supplier>> GetAllAsync(string? searchString = null);
        Task<Supplier?> GetByIdAsync(int id);
        Task AddAsync(Supplier supplier);
        Task UpdateAsync(Supplier supplier);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> HasGoodsAsync(int supplierId);
    }
}
