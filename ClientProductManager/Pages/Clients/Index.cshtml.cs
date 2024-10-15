using ClientProductManager.Core.Entities;
using ClientProductManager.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientProductManager.Core.Services.Contract;

namespace ClientProductManager.Pages.Clients
{
    public class IndexModel : PageModel
    {
        private readonly IClientService _clientService;

        [BindProperty]
        public List<Client> Clients { get; set; }

        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public int TotalClients { get; set; }
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(TotalClients, PageSize));

        public IndexModel(IClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task OnGetAsync(int pageIndex = 1)
        {
            CurrentPage = pageIndex;

            Clients = await _clientService.GetClientsAsync(CurrentPage, PageSize);
            TotalClients = await _clientService.GetTotalClientsCountAsync();
        }
    }
}
