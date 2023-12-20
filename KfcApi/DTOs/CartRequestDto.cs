using System.ComponentModel.DataAnnotations;

namespace KfcApi.DTOs
{
    public class CartRequestDto
    {
        [Required]
        public int ProductId { get; set; }
        public string UserId { get; set; }
        [Required]
        [Range(1, 99)]
        public int Quantity { get; set; }
        public string? Comments { get; set; }
        public string? AddOns { get; set; }
        public string? MenuAddOns { get; set; }
    }
}
