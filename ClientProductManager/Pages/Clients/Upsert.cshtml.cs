using ClientProductManager.Core.Entities;
using ClientProductManager.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClientProductManager.Core.ViewModels;
using ClientProductManager.Core.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClientProductManager.Core.Services.Contract;

namespace ClientProductManager.Pages.Clients
{
    public class UpsertModel : PageModel
    {
        private readonly IClientService _clientService;

        [BindProperty]
        public ClientViewModel ClientVM { get; set; }

        public UpsertModel(IClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task OnGetAsync(int? id)
        {
            if (id == null)
            {
                ClientVM = new ClientViewModel();
            }
            else
            {
                var client = await _clientService.GetClientByIdAsync(id.Value);
                if (client != null)
                {
                    ClientVM = new ClientViewModel
                    {
                        Id = client.Id,
                        Name = client.Name,
                        Code = client.Code,
                        State = ((int)client.State).ToString(),
                        Class = ((int)client.Class).ToString()
                    };
                }
            }

            PopulateDropdowns();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                PopulateDropdowns();
                return Page();
            }

            var clientEntity = new Client
            {
                Id = ClientVM.Id,
                Name = ClientVM.Name,
                Code = ClientVM.Code,
                State = Enum.Parse<ClientState>(ClientVM.State),
                Class = Enum.Parse<ClientClass>(ClientVM.Class)
            };

            await _clientService.AddOrUpdateClientAsync(clientEntity);
            return RedirectToPage("./Index");
        }

        private void PopulateDropdowns()
        {

            ClientVM.ClassList = Enum.GetValues(typeof(ClientClass))
      .Cast<ClientClass>()
      .Select(c => new SelectListItem
      {
          Value = ((int)c).ToString(), 
          Text = c.ToString()
      })
      .ToList();

            ClientVM.StateList = Enum.GetValues(typeof(ClientState))
                .Cast<ClientState>()
                .Select(s => new SelectListItem
                {
                    Value = ((int)s).ToString(),
                    Text = s.ToString() 
                })
                .ToList();


        }
    }
}