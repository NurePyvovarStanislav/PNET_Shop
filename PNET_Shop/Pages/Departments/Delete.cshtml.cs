using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PNET_Shop.Models;
using PNET_Shop.Repositories;

namespace PNET_Shop.Pages.Departments
{
    public class DeleteModel : PageModel
    {
        private readonly IDepartmentRepository _repository;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(IDepartmentRepository repository, ILogger<DeleteModel> logger)
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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var department = await _repository.GetByIdAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            if (await _repository.HasGoodsAsync(id))
            {
                Department = department;
                ModelState.AddModelError(string.Empty, "Неможливо видалити відділ, оскільки до нього прив'язані товари.");
                return Page();
            }

            await _repository.DeleteAsync(id);

            _logger.LogInformation("Видалено відділ: {Name} (Id: {DeptId})", department.Name, department.DeptId);

            return RedirectToPage("./Index");
        }
    }
}
