using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PNET_Shop.Repositories;
using CheckEntity = PNET_Shop.Models.Check;

namespace PNET_Shop.Pages.Checks
{
    public class DeleteModel : PageModel
    {
        private readonly ICheckRepository _repository;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(ICheckRepository repository, ILogger<DeleteModel> logger)
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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var check = await _repository.GetByIdAsync(id);

            if (check == null)
            {
                return NotFound();
            }

            if (await _repository.HasSalesAsync(id))
            {
                Check = check;
                ModelState.AddModelError(string.Empty, "Неможливо видалити чек, оскільки до нього прив'язані продажі.");
                return Page();
            }

            await _repository.DeleteAsync(id);

            _logger.LogInformation("Видалено чек: {CheckNo}", check.CheckNo);

            return RedirectToPage("./Index");
        }
    }
}
