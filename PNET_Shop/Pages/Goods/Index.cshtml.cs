using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PNET_Shop.Data;
using PNET_Shop.Models;

namespace PNET_Shop.Pages.Goods
{
    public class IndexModel : PageModel
    {
        private readonly ShopDbContext _context;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ShopDbContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IList<Good> Goods { get; set; } = new List<Good>();

        public string? SearchString { get; set; }

        public async Task OnGetAsync(string? searchString)
        {
            SearchString = searchString;

            var query = _context.Goods
                .Include(g => g.Department)
                .Include(g => g.Supplier)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(g =>
                    g.Name.Contains(searchString) ||
                    (g.Producer != null && g.Producer.Contains(searchString)));
            }

            Goods = await query.ToListAsync();

            _logger.LogInformation("Виконано перегляд товарів. Пошук: {SearchString}", searchString);
        }
    }
}
