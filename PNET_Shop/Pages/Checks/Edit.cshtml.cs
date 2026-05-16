using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PNET_Shop.Repositories;
using CheckEntity = PNET_Shop.Models.Check;

namespace PNET_Shop.Pages.Checks
{
    public class EditModel : PageModel
    {
        private readonly ICheckRepository _repository;
        private readonly ILogger<EditModel> _logger;

        public EditModel(ICheckRepository repository, ILogger<EditModel> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!await _repository.ExistsAsync(Check.CheckNo))
            {
                return NotFound();
            }

            await _repository.UpdateAsync(Check);

            _logger.LogInformation("Оновлено чек: {CheckNo}", Check.CheckNo);

            return RedirectToPage("./Index");
        }
    }
}
