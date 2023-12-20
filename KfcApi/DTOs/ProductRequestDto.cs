using System.ComponentModel.DataAnnotations;

namespace KfcApi.DTOs
{
    public class ProductRequestDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        public string? Description { get; set; }
        [Required]
        public bool HasMenu { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public int? HomeCategoryId { get; set; }
    }
}
