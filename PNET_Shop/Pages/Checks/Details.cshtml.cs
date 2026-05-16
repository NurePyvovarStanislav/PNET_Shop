using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PNET_Shop.Repositories;
using CheckEntity = PNET_Shop.Models.Check;

namespace PNET_Shop.Pages.Checks
{
    public class DetailsModel : PageModel
    {
        private readonly ICheckRepository _repository;

        public DetailsModel(ICheckRepository repository)
        {
            _repository = repository;
        }

        public CheckEntity Check { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var check = await _repository.GetByIdAsync(id);

            if (check == null)
            {
                return NotFound();
            }

            Check = check;
            return Page();
        }
    }
}
