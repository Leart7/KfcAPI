using KfcApi.Models.AbstractModelClasses;
using Microsoft.AspNetCore.Identity;

namespace KfcApi.Models
{
    public class Cart : BaseModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string? UserId { get; set; }
        public string? Comments { get; set; }
        public string? AddOns { get; set; }
        public string? MenuAddOns { get; set; }

        public Product? Product { get; set; }
    }
}
