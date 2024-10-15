using ClientProductManager.Core; // Include your core project namespace
using ClientProductManager.Core.Entities;
using ClientProductManager.Core.Services.Contract;
using ClientProductManager.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace ClientProductManager.Pages.ClientsProducts
{
    public class DetailsModel : PageModel
    {
        private readonly IClientProductService _clientProductService;

        [BindProperty]
        public ClientProductViewModel ClientProductVM { get; set; }

        public DetailsModel(IClientProductService clientProductService)
        {
            _clientProductService = clientProductService;
        }

        public async Task<IActionResult> OnGetAsync(int productId, int clientId)
        {
            var clientProduct = await _clientProductService.GetClientProductAsync(clientId, productId);

            if (clientProduct == null)
            {
                return NotFound();
            }

            ClientProductVM = new ClientProductViewModel
            {
                ProductName = clientProduct.Product.Name,
                ProductDescription = clientProduct.Product.Description,
                ClientName = clientProduct.Client.Name,
                ClientState = clientProduct.Client.State.ToString(),
                ClientClass = clientProduct.Client.Class.ToString(),
                ClientCode = clientProduct.Client.Code,
                ProductId = clientProduct.ProductId,
                StartDate = clientProduct.StartDate,
                EndDate = clientProduct.EndDate,
                License = clientProduct.License
            };

            return Page();
        }
    }
}
