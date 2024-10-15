using ClientProductManager.Core.ViewModels;
using ClientProductManager.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClientProductManager.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClientProductManager.Core.Services.Contract;

namespace ClientProductManager.Pages.ClientsProducts
{
    public class UpsertModel : PageModel
    {
        private readonly IClientProductService _clientProductService;

        [BindProperty]
        public ClientProductViewModel ClientProductVM { get; set; }

        public UpsertModel(IClientProductService clientProductService)
        {
            _clientProductService = clientProductService;
        }

        public async Task OnGetAsync(int? clientId, int? productId)
        {
            ClientProductVM = new ClientProductViewModel
            {
                StartDate = DateTime.Now,
                EndDate = null
            };

            await PopulateDropdowns();

            if (clientId.HasValue && productId.HasValue)
            {
                var clientProduct = await _clientProductService.GetClientProductAsync(clientId.Value, productId.Value);
                if (clientProduct != null)
                {
                    ClientProductVM.ClientId = clientProduct.ClientId;
                    ClientProductVM.ProductId = clientProduct.ProductId;
                    ClientProductVM.StartDate = clientProduct.StartDate;
                    ClientProductVM.EndDate = clientProduct.EndDate;
                    ClientProductVM.License = clientProduct.License;
                }
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
              await PopulateDropdowns();
                return Page();
            }

            var clientProductEntity = new ClientProducts
            {
                ClientId = ClientProductVM.ClientId,
                ProductId = ClientProductVM.ProductId,
                StartDate = ClientProductVM.StartDate,
                EndDate = ClientProductVM.EndDate,
                License = ClientProductVM.License
            };

            await _clientProductService.AddOrUpdateClientProductAsync(clientProductEntity);

            return RedirectToPage("Index");
        }
        private async Task PopulateDropdowns()
        {
            ClientProductVM.ClientList = await _clientProductService.GetClientsDropdownAsync();
            ClientProductVM.ProductList = await _clientProductService.GetProductsDropdownAsync();
        }
    }
}