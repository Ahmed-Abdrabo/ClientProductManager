using ClientProductManager.Core.Entities;
using ClientProductManager.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClientProductManager.Core.Services.Contract;

namespace ClientProductManager.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private readonly IProductService _productService;

        [BindProperty]
        public Product Product { get; set; }

        public DeleteModel(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Product = await _productService.GetProductByIdAsync(id);
            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (await _productService.ProductHasRelatedClientProductsAsync(id))
            {
                ModelState.AddModelError("", "Cannot delete product because it has related client products.");
                return Page();
            }

            await _productService.DeleteProductAsync(id);
            return RedirectToPage("./Index");
        }

    }
}
