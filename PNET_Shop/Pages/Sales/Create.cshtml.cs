using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PNET_Shop.Data;
using PNET_Shop.Models;
using PNET_Shop.Repositories;

namespace PNET_Shop.Pages.Sales
{
    public class CreateModel : PageModel
    {
        private readonly ISaleRepository _repository;
        private readonly ShopDbContext _context;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(ISaleRepository repository, ShopDbContext context, ILogger<CreateModel> logger)
        {
            _repository = repository;
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Sale Sale { get; set; } = new();

        public SelectList Goods { get; set; } = default!;
        public SelectList Checks { get; set; } = default!;

        public void OnGet()
        {
            Sale = new Sale
            {
                DateSale = DateTime.Now,
                Quantity = 1
            };

            LoadSelectLists();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                LoadSelectLists();
                return Page();
            }

            await _repository.AddAsync(Sale);

            _logger.LogInformation(
                "Додано продаж (Id: {SaleId}, GoodId: {GoodId}, CheckNo: {CheckNo})",
                Sale.SaleId,
                Sale.GoodId,
                Sale.CheckNo);

            return RedirectToPage("./Index");
        }

        private void LoadSelectLists()
        {
            Goods = new SelectList(
                _context.Goods.OrderBy(g => g.Name),
                nameof(Good.GoodId),
                nameof(Good.Name),
                Sale.GoodId);

            Checks = new SelectList(
                _context.Checks
                    .OrderBy(c => c.CheckNo)
                    .AsEnumerable()
                    .Select(c => new
                    {
                        c.CheckNo,
                        Text = $"№{c.CheckNo} — {c.CheckDate:dd.MM.yyyy}"
                    }),
                "CheckNo",
                "Text",
                Sale.CheckNo);
        }
    }
}
