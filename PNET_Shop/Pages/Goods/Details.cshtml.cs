using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PNET_Shop.Data;
using PNET_Shop.Models;

namespace PNET_Shop.Pages.Goods
{
    public class DetailsModel : PageModel
    {
        private readonly ShopDbContext _context;

        public DetailsModel(ShopDbContext context)
        {
            _context = context;
        }

        public Good Good { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var good = await _context.Goods
                .Include(g => g.Department)
                .Include(g => g.Supplier)
                .FirstOrDefaultAsync(m => m.GoodId == id);

            if (good == null)
            {
                return NotFound();
            }

            Good = good;

            return Page();
        }
    }
}
