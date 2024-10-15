using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientProductManager.Core.Enums;

namespace ClientProductManager.Core.Entities
{
    public class Client
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Client Name is required.")]
        [StringLength(50, ErrorMessage = "Client Name must be less than 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Client Code is required.")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Client Code must be exactly 9 digits.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Client Code must be numeric.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Client Class is required.")]
        public ClientClass Class { get; set; }

        [Required(ErrorMessage = "Client State is required.")]
        public ClientState State { get; set; }
        public ICollection<ClientProducts>? ClientProducts { get; set; }
    }
}
