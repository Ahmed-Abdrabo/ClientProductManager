using ClientProductManager.Core.Entities;
using ClientProductManager.Core.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientProductManager.Core.Services.Contract
{
    public interface IClientProductService
    {
        Task<List<ClientProductViewModel>> GetClientProductsAsync(int clientId);
        Task<List<ClientProducts>> GetAllClientProductsAsync();
        Task<ClientProducts> GetClientProductAsync(int clientId, int productId);
        Task AddOrUpdateClientProductAsync(ClientProducts clientProduct);
        Task DeleteClientProductAsync(int clientId, int productId);

        Task<List<SelectListItem>> GetClientsDropdownAsync();
        Task<List<SelectListItem>> GetProductsDropdownAsync();

    }
}
