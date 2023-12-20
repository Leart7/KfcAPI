using KfcApi.Models;
using System.ComponentModel.DataAnnotations;

namespace KfcApi.DTOs
{
    public class OrderRequestDto
    {
        [Required]
        public int OrderUserId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        [Range(0, 99)]
        public int Quantity { get; set; }
        public string? Comments { get; set; }
        public string? AddOns { get; set; }
        public string? MenuAddOns { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        public string AddressNotes { get; set; }
        public string HouseNumber { get; set; }
        public string KfcLocation { get; set; }

    }
}
