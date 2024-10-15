using ClientProductManager.Core.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientProductManager.Core.ViewModels
{
    public class ClientViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(9, MinimumLength = 9)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Client Code must be numeric.")]
        public string Code { get; set; }

        [Required]
        public string Class { get; set; }

        [Required]
        public string State { get; set; }

        [ValidateNever]
        public List<SelectListItem> ClassList { get; set; }
        [ValidateNever]
        public List<SelectListItem> StateList { get; set; } 

        public List<ClientProductViewModel> ClientProducts { get; set; } = new List<ClientProductViewModel>();
    }
}
