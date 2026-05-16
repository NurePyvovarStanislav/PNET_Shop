using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PNET_Shop.Models;
using PNET_Shop.Repositories;

namespace PNET_Shop.Pages.Suppliers
{
    public class DetailsModel : PageModel
    {
        private readonly ISupplierRepository _repository;

        public DetailsModel(ISupplierRepository repository)
        {
            _repository = repository;
        }

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
    }
}
