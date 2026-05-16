using PNET_Shop.Models;

namespace PNET_Shop.Repositories
{
    public interface ICheckRepository
    {
        Task<List<Check>> GetAllAsync(string? searchString = null);
        Task<Check?> GetByIdAsync(int id);
        Task AddAsync(Check check);
        Task UpdateAsync(Check check);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> HasSalesAsync(int checkNo);
    }
}
