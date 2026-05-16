using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PNET_Shop.Models;
using PNET_Shop.Repositories;

namespace PNET_Shop.Pages.Goods
{
    public class DeleteModel : PageModel
    {
        private readonly IGoodRepository _repository;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(IGoodRepository repository, ILogger<DeleteModel> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [BindProperty]
        public Good Good { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var good = await _repository.GetByIdAsync(id);

            if (good == null)
            {
                return NotFound();
            }

            Good = good;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var good = await _repository.GetByIdAsync(id);

            if (good == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);

            _logger.LogInformation("Видалено товар: {GoodName} (Id: {GoodId})", good.Name, good.GoodId);

            return RedirectToPage("./Index");
        }
    }
}
