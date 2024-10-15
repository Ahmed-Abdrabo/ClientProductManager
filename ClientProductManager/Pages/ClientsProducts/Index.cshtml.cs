using ClientProductManager.Core.Entities;
using ClientProductManager.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClientProductManager.Core.Services.Contract;

namespace ClientProductManager.Pages.ClientsProducts
{
    public class IndexModel : PageModel
    {

        private readonly IClientProductService _clientProductService;

        [BindProperty]
        public List<ClientProducts> ClientProducts { get; set; }

        public IndexModel(IClientProductService clientProductService)
        {
            _clientProductService = clientProductService;
        }

        public async Task OnGetAsync()
        {
            ClientProducts = await _clientProductService.GetAllClientProductsAsync();
        }
    }
}
