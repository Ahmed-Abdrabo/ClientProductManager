using ClientProductManager.Core;
using ClientProductManager.Core.Entities;
using ClientProductManager.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientProductManager.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            return _unitOfWork.Repository<Client>().Get(c => c.Id == id);
        }

        public async Task<List<Client>> GetClientsAsync(int pageIndex, int pageSize)
        {
            return _unitOfWork.Repository<Client>()
                .GetAll()
                .OrderBy(c => c.Code)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public async Task<int> GetTotalClientsCountAsync()
        {
            return _unitOfWork.Repository<Client>().GetAll().Count();
        }

        public async Task AddOrUpdateClientAsync(Client client)
        {
            if (client.Id == 0)
            {
                _unitOfWork.Repository<Client>().Add(client);
            }
            else
            {
                _unitOfWork.Repository<Client>().Update(client);
            }

            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteClientAsync(int id)
        {
            var client = _unitOfWork.Repository<Client>().Get(c => c.Id == id);
            if (client != null)
            {
                _unitOfWork.Repository<Client>().Remove(client);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<bool> HasRelatedProductsAsync(int clientId)
        {
            return _unitOfWork.Repository<ClientProducts>()
                .GetAll(cp => cp.ClientId == clientId)
                .Any();
        }
    }
}
