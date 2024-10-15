using ClientProductManager.Core;
using ClientProductManager.Core.Entities;
using ClientProductManager.Core.Services.Contract;
using ClientProductManager.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientProductManager.Pages.Clients
{
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IClientService _clientService;
        private readonly IClientProductService _clientProductService;

        public DetailsModel(IClientService clientService, IClientProductService clientProductService)
        {
            _clientService = clientService;
            _clientProductService = clientProductService;
        }

        [BindProperty]
        public ClientViewModel ClientVM { get; set; }
        public List<ClientProductViewModel> ClientProducts { get; set; } = new List<ClientProductViewModel>();

        public async Task OnGetAsync(int id)
        {
            var client = await _clientService.GetClientByIdAsync(id);
            if (client != null)
            {
                ClientVM = new ClientViewModel
                {
                    Id = client.Id,
                    Name = client.Name,
                    Code = client.Code,
                    State = client.State.ToString(),
                    Class = client.Class.ToString()
                };

                ClientProducts = await _clientProductService.GetClientProductsAsync(id);
            }
        }
    
    }
}
