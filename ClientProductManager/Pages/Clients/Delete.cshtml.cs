using ClientProductManager.Core;
using ClientProductManager.Core.Entities;
using ClientProductManager.Core.Services.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClientProductManager.Pages.Clients
{
    public class DeleteModel : PageModel
    {
        private readonly IClientService _clientService;

        [BindProperty]
        public Client Client { get; set; }

        public DeleteModel(IClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Client = await _clientService.GetClientByIdAsync(id);
            if (Client == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var hasRelatedProducts = await _clientService.HasRelatedProductsAsync(id);
            if (hasRelatedProducts)
            {
                ModelState.AddModelError("", "Cannot delete client because it has related products.");
                return Page();
            }

            await _clientService.DeleteClientAsync(id);
            return RedirectToPage("./Index");
        }

    }
}
