using Microsoft.AspNetCore.Mvc.RazorPages;
using PNET_Shop.Models;
using PNET_Shop.Repositories;

namespace PNET_Shop.Pages.Departments
{
    public class IndexModel : PageModel
    {
        private readonly IDepartmentRepository _repository;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IDepartmentRepository repository, ILogger<IndexModel> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public IList<Department> Departments { get; set; } = new List<Department>();

        public async Task OnGetAsync()
        {
            Departments = await _repository.GetAllAsync();
            _logger.LogInformation("Перегляд списку відділів");
        }
    }
}
