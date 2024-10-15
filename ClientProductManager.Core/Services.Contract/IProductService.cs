using ClientProductManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientProductManager.Core.Services.Contract
{
    public interface IProductService
    {
        Task<List<Product>> GetPagedProductsAsync(int pageIndex, int pageSize);
        Task<int> GetTotalProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task AddOrUpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task<bool> ProductHasRelatedClientProductsAsync(int productId);
    }

}
