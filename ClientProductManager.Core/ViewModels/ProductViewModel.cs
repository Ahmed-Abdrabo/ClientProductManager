using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientProductManager.Core.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Name is required.")]
        [StringLength(50, ErrorMessage = "Product Name must be less than 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(150, ErrorMessage = "Description must be less than 150 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "IsActive is required.")]
        public bool IsActive { get; set; }
        public List<ClientProductViewModel> ClientProducts { get; set; } = new List<ClientProductViewModel>();

    }
}
