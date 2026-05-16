using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PNET_Shop.Models;
using PNET_Shop.Repositories;

namespace PNET_Shop.Pages.Suppliers
{
    public class DeleteModel : PageModel
    {
        private readonly ISupplierRepository _repository;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(ISupplierRepository repository, ILogger<DeleteModel> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [BindProperty]
        public Supplier Supplier { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var supplier = await _repository.GetByIdAsync(id);

            if (supplier == null)
            {
                return NotFound();
            }

            Supplier = supplier;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var supplier = await _repository.GetByIdAsync(id);

            if (supplier == null)
            {
                return NotFound();
            }

            if (await _repository.HasGoodsAsync(id))
            {
                Supplier = supplier;
                ModelState.AddModelError(string.Empty, "Неможливо видалити постачальника, оскільки до нього прив'язані товари.");
                return Page();
            }

            await _repository.DeleteAsync(id);

            _logger.LogInformation("Видалено постачальника: {Name} (Id: {SupplierId})", supplier.Name, supplier.SupplierId);

            return RedirectToPage("./Index");
        }
    }
}
