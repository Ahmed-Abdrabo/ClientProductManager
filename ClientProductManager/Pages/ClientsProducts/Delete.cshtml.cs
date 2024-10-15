using ClientProductManager.Core;
using ClientProductManager.Core.Entities;
using ClientProductManager.Core.Services.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;

namespace ClientProductManager.Pages.ClientsProducts
{
    public class DeleteModel : PageModel
    {
        private readonly IClientProductService _clientProductService;

        public ClientProducts ClientProduct { get; set; }

        public DeleteModel(IClientProductService clientProductService)
        {
            _clientProductService = clientProductService;
        }

        public async Task<IActionResult> OnGetAsync(int clientId, int productId)
        {
            ClientProduct = await _clientProductService.GetClientProductAsync(clientId, productId);

            if (ClientProduct == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int clientId, int productId)
        {
            await _clientProductService.DeleteClientProductAsync(clientId, productId);
            return RedirectToPage("Index");
        }
    }

}
