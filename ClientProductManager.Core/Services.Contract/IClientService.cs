using ClientProductManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientProductManager.Core.Services.Contract
{
    public interface IClientService
    {
        Task<Client> GetClientByIdAsync(int id);
        Task<List<Client>> GetClientsAsync(int pageIndex, int pageSize);
        Task<int> GetTotalClientsCountAsync();
        Task AddOrUpdateClientAsync(Client client);
        Task DeleteClientAsync(int id);
        Task<bool> HasRelatedProductsAsync(int clientId);
    }
}
