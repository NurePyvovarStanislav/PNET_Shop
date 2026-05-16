using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PNET_Shop.Data;
using PNET_Shop.Models;
using PNET_Shop.Repositories;

namespace PNET_Shop.Pages.Goods
{
    public class EditModel : PageModel
    {
        private readonly IGoodRepository _repository;
        private readonly ShopDbContext _context;
        private readonly ILogger<EditModel> _logger;

        public EditModel(IGoodRepository repository, ShopDbContext context, ILogger<EditModel> logger)
        {
            _repository = repository;
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Good Good { get; set; } = default!;

        public SelectList Departments { get; set; } = default!;
        public SelectList Suppliers { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var good = await _repository.GetByIdAsync(id);

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

            if (!await _repository.ExistsAsync(Good.GoodId))
            {
                return NotFound();
            }

            await _repository.UpdateAsync(Good);

            _logger.LogInformation("Оновлено товар: {GoodName} (Id: {GoodId})", Good.Name, Good.GoodId);

            return RedirectToPage("./Index");
        }

        private void LoadSelectLists()
        {
            Departments = new SelectList(_context.Departments, "DeptId", "Name", Good.DeptId);
            Suppliers = new SelectList(_context.Suppliers, "SupplierId", "Name", Good.SupplierId);
        }
    }
}
