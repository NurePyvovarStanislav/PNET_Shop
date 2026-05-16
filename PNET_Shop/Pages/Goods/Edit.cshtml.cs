using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PNET_Shop.Data;
using PNET_Shop.Models;

namespace PNET_Shop.Pages.Goods
{
    public class EditModel : PageModel
    {
        private readonly ShopDbContext _context;
        private readonly ILogger<EditModel> _logger;

        public EditModel(ShopDbContext context, ILogger<EditModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Good Good { get; set; } = default!;

        public SelectList Departments { get; set; } = default!;
        public SelectList Suppliers { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var good = await _context.Goods.FindAsync(id);

            if (good == null)
            {
                return NotFound();
            }

            Good = good;
            LoadSelectLists();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                LoadSelectLists();
                return Page();
            }

            _context.Attach(Good).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Оновлено товар: {GoodName}", Good.Name);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoodExists(Good.GoodId))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToPage("./Index");
        }

        private bool GoodExists(int id)
        {
            return _context.Goods.Any(e => e.GoodId == id);
        }

        private void LoadSelectLists()
        {
            Departments = new SelectList(_context.Departments, "DeptId", "Name");
            Suppliers = new SelectList(_context.Suppliers, "SupplierId", "Name");
        }
    }
}
