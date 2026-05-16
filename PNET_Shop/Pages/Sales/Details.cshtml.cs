using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PNET_Shop.Models;
using PNET_Shop.Repositories;

namespace PNET_Shop.Pages.Sales
{
    public class DetailsModel : PageModel
    {
        private readonly ISaleRepository _repository;

        public DetailsModel(ISaleRepository repository)
        {
            _repository = repository;
        }

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
    }
}
