using ClientProductManager.Core.Entities;
using ClientProductManager.Core;
using ClientProductManager.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientProductManager.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Product>> GetPagedProductsAsync(int pageIndex, int pageSize)
        {
            return _unitOfWork.Repository<Product>()
                .GetAll()
                .OrderBy(c => c.Name)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public async Task<int> GetTotalProductsAsync()
        {
            return _unitOfWork.Repository<Product>().GetAll().Count();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return _unitOfWork.Repository<Product>().Get(u => u.Id == id, includeProperties: "ClientProducts");
        }

        public async Task AddOrUpdateProductAsync(Product product)
        {
            if (product.Id == 0)
            {
                _unitOfWork.Repository<Product>().Add(product);
            }
            else
            {
                _unitOfWork.Repository<Product>().Update(product);
            }
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = _unitOfWork.Repository<Product>().Get(p => p.Id == id);
            if (product != null)
            {
                _unitOfWork.Repository<Product>().Remove(product);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<bool> ProductHasRelatedClientProductsAsync(int productId)
        {
            return _unitOfWork.Repository<ClientProducts>()
                .GetAll(cp => cp.ProductId == productId)
                .Any();
        }
    }
}
