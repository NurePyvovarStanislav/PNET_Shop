using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PNET_Shop.Models;
using PNET_Shop.Repositories;

namespace PNET_Shop.Pages.Suppliers
{
    public class CreateModel : PageModel
    {
        private readonly ISupplierRepository _repository;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(ISupplierRepository repository, ILogger<CreateModel> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [BindProperty]
        public Supplier Supplier { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _repository.AddAsync(Supplier);

            _logger.LogInformation("Додано постачальника: {Name} (Id: {SupplierId})", Supplier.Name, Supplier.SupplierId);

            return RedirectToPage("./Index");
        }
    }
}
