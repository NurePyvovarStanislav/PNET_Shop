using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PNET_Shop.Data;
using PNET_Shop.Models;

namespace PNET_Shop.Pages.Goods
{
    public class CreateModel : PageModel
    {
        private readonly ShopDbContext _context;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(ShopDbContext context, ILogger<CreateModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Good Good { get; set; } = new();

        public SelectList Departments { get; set; } = default!;
        public SelectList Suppliers { get; set; } = default!;

        public void OnGet()
        {
            LoadSelectLists();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                LoadSelectLists();
                return Page();
            }

            _context.Goods.Add(Good);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Додано новий товар: {GoodName}", Good.Name);

            return RedirectToPage("./Index");
        }

        private void LoadSelectLists()
        {
            Departments = new SelectList(_context.Departments, "DeptId", "Name");
            Suppliers = new SelectList(_context.Suppliers, "SupplierId", "Name");
        }
    }
}
