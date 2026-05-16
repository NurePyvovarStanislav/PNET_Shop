using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PNET_Shop.Models;
using PNET_Shop.Repositories;

namespace PNET_Shop.Pages.Departments
{
    public class EditModel : PageModel
    {
        private readonly IDepartmentRepository _repository;
        private readonly ILogger<EditModel> _logger;

        public EditModel(IDepartmentRepository repository, ILogger<EditModel> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [BindProperty]
        public Department Department { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var department = await _repository.GetByIdAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            Department = department;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!await _repository.ExistsAsync(Department.DeptId))
            {
                return NotFound();
            }

            await _repository.UpdateAsync(Department);

            _logger.LogInformation("Оновлено відділ: {Name} (Id: {DeptId})", Department.Name, Department.DeptId);

            return RedirectToPage("./Index");
        }
    }
}
