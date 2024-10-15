using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClientProductManager.Core.ViewModels
{
    public class ClientProductViewModel
    {
        public int ClientId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public string? ClientName { get; set; }
        public string? ClientState { get; set; }
        public string? ClientClass { get; set; }
        public string? ClientCode { get; set; }
        public int ProductId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string License { get; set; }
        [ValidateNever]
        public List<SelectListItem> ClientList { get; set; }
        [ValidateNever]
        public List<SelectListItem> ProductList { get; set; }
    }
}
