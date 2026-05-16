using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PNET_Shop.Models;
using PNET_Shop.Repositories;

namespace PNET_Shop.Pages.Departments
{
    public class CreateModel : PageModel
    {
        private readonly IDepartmentRepository _repository;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(IDepartmentRepository repository, ILogger<CreateModel> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [BindProperty]
        public Department Department { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Department.DeptId = 0;
            await _repository.AddAsync(Department);

            _logger.LogInformation("Додано відділ: {Name} (Id: {DeptId})", Department.Name, Department.DeptId);

            return RedirectToPage("./Index");
        }
    }
}
