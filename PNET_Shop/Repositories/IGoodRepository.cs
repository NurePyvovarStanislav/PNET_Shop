using PNET_Shop.Models;

namespace PNET_Shop.Repositories
{
    public interface IGoodRepository
    {
        Task<List<Good>> GetAllAsync(string? searchString = null);
        Task<Good?> GetByIdAsync(int id);
        Task AddAsync(Good good);
        Task UpdateAsync(Good good);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> HasSalesAsync(int goodId);
    }
}
