using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PNET_Shop.Models;
using PNET_Shop.Repositories;

namespace PNET_Shop.Pages.Sales
{
    public class DeleteModel : PageModel
    {
        private readonly ISaleRepository _repository;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(ISaleRepository repository, ILogger<DeleteModel> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [BindProperty]
        public Sale Sale { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var sale = await _repository.GetByIdAsync(id);

            if (sale == null)
            {
                return NotFound();
            }

            Sale = sale;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var sale = await _repository.GetByIdAsync(id);

            if (sale == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);

            _logger.LogInformation(
                "Видалено продаж (Id: {SaleId}, Good: {GoodName})",
                sale.SaleId,
                sale.Good?.Name);

            return RedirectToPage("./Index");
        }
    }
}
