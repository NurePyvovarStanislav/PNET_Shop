using Microsoft.AspNetCore.Mvc.RazorPages;
using PNET_Shop.Models;
using PNET_Shop.Repositories;

namespace PNET_Shop.Pages.Goods
{
    public class IndexModel : PageModel
    {
        private readonly IGoodRepository _repository;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IGoodRepository repository, ILogger<IndexModel> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public IList<Good> Goods { get; set; } = new List<Good>();

        public string? SearchString { get; set; }

        public async Task OnGetAsync(string? searchString)
        {
            SearchString = searchString;
            Goods = await _repository.GetAllAsync(searchString);

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                _logger.LogInformation("Виконано пошук товарів за запитом: {SearchString}", searchString);
            }
            else
            {
                _logger.LogInformation("Виконано перегляд списку товарів");
            }
        }
    }
}
