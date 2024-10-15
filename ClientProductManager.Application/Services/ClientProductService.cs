using ClientProductManager.Core.Entities;
using ClientProductManager.Core.ViewModels;
using ClientProductManager.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientProductManager.Core.Services.Contract;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClientProductManager.Application.Services
{
    public class ClientProductService: IClientProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClientProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ClientProductViewModel>> GetClientProductsAsync(int clientId)
        {
            return _unitOfWork.Repository<ClientProducts>()
                .GetAll(cp => cp.ClientId == clientId, includeProperties: "Product")
                .Select(cp => new ClientProductViewModel
                {
                    ProductName = cp.Product.Name,
                    ProductDescription = cp.Product.Description,
                    StartDate = cp.StartDate,
                    EndDate = cp.EndDate,
                    License = cp.License,
                    ClientId = cp.ClientId,
                    ProductId = cp.ProductId
                })
                .OrderBy(cp => cp.ProductName)
                .ToList();
        }
        public async Task<List<ClientProducts>> GetAllClientProductsAsync()
        {
            return _unitOfWork.Repository<ClientProducts>().GetAll(includeProperties: "Client,Product").ToList();
        }

        public async Task<ClientProducts> GetClientProductAsync(int clientId, int productId)
        {
            return _unitOfWork.Repository<ClientProducts>().Get(
                cp => cp.ClientId == clientId && cp.ProductId == productId,
                includeProperties: "Client,Product");
        }

        public async Task AddOrUpdateClientProductAsync(ClientProducts clientProduct)
        {
            var existingClientProduct = _unitOfWork.Repository<ClientProducts>().Get(
                cp => cp.ClientId == clientProduct.ClientId && cp.ProductId == clientProduct.ProductId);

            if (existingClientProduct == null)
            {
                _unitOfWork.Repository<ClientProducts>().Add(clientProduct);
            }
            else
            {
                existingClientProduct.StartDate = clientProduct.StartDate;
                existingClientProduct.EndDate = clientProduct.EndDate;
                existingClientProduct.License = clientProduct.License;
                _unitOfWork.Repository<ClientProducts>().Update(existingClientProduct);
            }

            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteClientProductAsync(int clientId, int productId)
        {
            var clientProduct = _unitOfWork.Repository<ClientProducts>().Get(cp => cp.ClientId == clientId && cp.ProductId == productId);
            if (clientProduct != null)
            {
                _unitOfWork.Repository<ClientProducts>().Remove(clientProduct);
                await _unitOfWork.CompleteAsync();
            }
        }
        public async Task<List<SelectListItem>> GetClientsDropdownAsync()
        {
            var clients = _unitOfWork.Repository<Client>().GetAll();
            return clients.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();
        }

        public async Task<List<SelectListItem>> GetProductsDropdownAsync()
        {
            var products = _unitOfWork.Repository<Product>().GetAll(p => p.IsActive);
            return products.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name
            }).ToList();
        }
    }

}
