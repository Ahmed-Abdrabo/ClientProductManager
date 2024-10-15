using ClientProductManager.Core.Entities;
using ClientProductManager.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClientProductManager.Core.Services.Contract;

namespace ClientProductManager.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;

        [BindProperty]
        public List<Product> Products { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public int TotalProducts { get; set; }
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(TotalProducts, PageSize));

        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }

        public async Task OnGetAsync(int pageIndex = 1)
        {
            CurrentPage = pageIndex;
            Products = await _productService.GetPagedProductsAsync(pageIndex, PageSize);
            TotalProducts = await _productService.GetTotalProductsAsync();
        }
    }
}
