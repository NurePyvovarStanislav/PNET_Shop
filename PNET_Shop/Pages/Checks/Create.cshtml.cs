using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PNET_Shop.Repositories;
using CheckEntity = PNET_Shop.Models.Check;

namespace PNET_Shop.Pages.Checks
{
    public class CreateModel : PageModel
    {
        private readonly ICheckRepository _repository;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(ICheckRepository repository, ILogger<CreateModel> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [BindProperty]
        public CheckEntity Check { get; set; } = new();

        public void OnGet()
        {
            Check = new CheckEntity
            {
                CheckDate = DateTime.Now
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Check.CheckNo = 0;
            await _repository.AddAsync(Check);

            _logger.LogInformation("Додано чек: {CheckNo}", Check.CheckNo);

            return RedirectToPage("./Index");
        }
    }
}
