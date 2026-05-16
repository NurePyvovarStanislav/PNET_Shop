using PNET_Shop.Models;

namespace PNET_Shop.Repositories
{
    public interface ISaleRepository
    {
        Task<List<Sale>> GetAllAsync(string? searchString = null);
        Task<Sale?> GetByIdAsync(int id);
        Task AddAsync(Sale sale);
        Task UpdateAsync(Sale sale);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
