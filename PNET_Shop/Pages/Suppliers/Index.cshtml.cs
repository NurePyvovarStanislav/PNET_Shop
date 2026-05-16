using Microsoft.AspNetCore.Mvc.RazorPages;
using PNET_Shop.Models;
using PNET_Shop.Repositories;

namespace PNET_Shop.Pages.Suppliers
{
    public class IndexModel : PageModel
    {
        private readonly ISupplierRepository _repository;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ISupplierRepository repository, ILogger<IndexModel> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public IList<Supplier> Suppliers { get; set; } = new List<Supplier>();

        public async Task OnGetAsync()
        {
            Suppliers = await _repository.GetAllAsync();
            _logger.LogInformation("Перегляд списку постачальників");
        }
    }
}
