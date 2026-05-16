using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PNET_Shop.Models;
using PNET_Shop.Repositories;

namespace PNET_Shop.Pages.Departments
{
    public class DetailsModel : PageModel
    {
        private readonly IDepartmentRepository _repository;

        public DetailsModel(IDepartmentRepository repository)
        {
            _repository = repository;
        }

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
    }
}
