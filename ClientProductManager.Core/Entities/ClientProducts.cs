using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientProductManager.Core.Entities
{
    public class ClientProducts
    {
        public int ClientId { get; set; }
        public Client Client { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        [StringLength(255)]
        public string License { get; set; }
    }
}
