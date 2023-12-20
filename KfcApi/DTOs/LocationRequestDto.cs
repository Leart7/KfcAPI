using System.ComponentModel.DataAnnotations;

namespace KfcApi.DTOs
{
    public class LocationRequestDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
        [Required]
        public string OpeningHour { get; set; }
        [Required]
        public string ClosingHour { get; set; }
        public string? City { get; set; }
    }
}
