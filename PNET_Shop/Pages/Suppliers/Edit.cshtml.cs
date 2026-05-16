using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PNET_Shop.Models;
using PNET_Shop.Repositories;

namespace PNET_Shop.Pages.Suppliers
{
    public class EditModel : PageModel
    {
        private readonly ISupplierRepository _repository;
        private readonly ILogger<EditModel> _logger;

        public EditModel(ISupplierRepository repository, ILogger<EditModel> logger)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!await _repository.ExistsAsync(Supplier.SupplierId))
            {
                return NotFound();
            }

            await _repository.UpdateAsync(Supplier);

            _logger.LogInformation("Оновлено постачальника: {Name} (Id: {SupplierId})", Supplier.Name, Supplier.SupplierId);

            return RedirectToPage("./Index");
        }
    }
}
