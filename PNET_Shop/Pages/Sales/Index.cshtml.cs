using Microsoft.AspNetCore.Mvc.RazorPages;
using PNET_Shop.Models;
using PNET_Shop.Repositories;

namespace PNET_Shop.Pages.Sales
{
    public class IndexModel : PageModel
    {
        private readonly ISaleRepository _repository;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ISaleRepository repository, ILogger<IndexModel> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public IList<Sale> Sales { get; set; } = new List<Sale>();

        public async Task OnGetAsync()
        {
            Sales = await _repository.GetAllAsync();
            _logger.LogInformation("Перегляд списку продажів");
        }
    }
}
