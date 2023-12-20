using System.ComponentModel.DataAnnotations;

namespace KfcApi.DTOs
{
    public class AddressRequestDto
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string AddressName { get; set; }
        public string? HouseNumber { get; set; }
        public string? AddressNotes { get; set; }
        [Required]
        public string Type { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
