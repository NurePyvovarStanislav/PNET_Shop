using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PNET_Shop.Models;
using PNET_Shop.Repositories;

namespace PNET_Shop.Pages.Goods
{
    public class DetailsModel : PageModel
    {
        private readonly IGoodRepository _repository;

        public DetailsModel(IGoodRepository repository)
        {
            _repository = repository;
        }

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
    }
}
