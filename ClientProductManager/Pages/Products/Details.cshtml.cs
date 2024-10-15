using ClientProductManager.Core;
using ClientProductManager.Core.Entities;
using ClientProductManager.Core.Services.Contract;
using ClientProductManager.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClientProductManager.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly IProductService _productService;

        public ProductViewModel ProductVM { get; set; }

        public DetailsModel(IProductService productService)
        {
            _productService = productService;
        }

        public async Task OnGetAsync(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            ProductVM = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                IsActive = product.IsActive
            };
        }
    }
}
