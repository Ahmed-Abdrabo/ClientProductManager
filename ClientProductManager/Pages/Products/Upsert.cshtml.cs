using ClientProductManager.Core.Entities;
using ClientProductManager.Core.Enums;
using ClientProductManager.Core.ViewModels;
using ClientProductManager.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClientProductManager.Core.Services.Contract;

namespace ClientProductManager.Pages.Products
{
    public class UpsertModel : PageModel
    {
        private readonly IProductService _productService;

        [BindProperty]
        public ProductViewModel ProductVM { get; set; }

        public UpsertModel(IProductService productService)
        {
            _productService = productService;
        }

        public async Task OnGetAsync(int? id)
        {
            ProductVM = new ProductViewModel();

            if (id!=null && id!=0)
            {
                var product = await _productService.GetProductByIdAsync(id.Value);
                ProductVM = new ProductViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    IsActive = product.IsActive
                };
            }
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var productEntity = new Product
            {
                Id = ProductVM.Id,
                Name = ProductVM.Name,
                Description = ProductVM.Description,
                IsActive = ProductVM.IsActive
            };

            await _productService.AddOrUpdateProductAsync(productEntity);
            return RedirectToPage("Index");
        }

    }
}
