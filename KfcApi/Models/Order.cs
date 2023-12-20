using KfcApi.Models.AbstractModelClasses;

namespace KfcApi.Models
{
    public class Order : BaseModel
    {
        public int OrderUserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string? Comments { get; set; }
        public string? AddOns { get; set; }
        public string? MenuAddOns { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string AddressNotes { get; set; }
        public string HouseNumber { get; set; }
        public string KfcLocation { get; set; }

        public virtual OrderUser OrderUser { get; set; }
        public virtual Product Product { get; set; }
    }
}
