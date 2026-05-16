using Microsoft.AspNetCore.Mvc.RazorPages;
using PNET_Shop.Repositories;
using CheckEntity = PNET_Shop.Models.Check;

namespace PNET_Shop.Pages.Checks
{
    public class IndexModel : PageModel
    {
        private readonly ICheckRepository _repository;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ICheckRepository repository, ILogger<IndexModel> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public IList<CheckEntity> Checks { get; set; } = new List<CheckEntity>();

        public async Task OnGetAsync()
        {
            Checks = await _repository.GetAllAsync();
            _logger.LogInformation("Перегляд списку чеків");
        }
    }
}
