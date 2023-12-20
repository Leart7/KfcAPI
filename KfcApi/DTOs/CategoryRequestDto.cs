using System.ComponentModel.DataAnnotations;

namespace KfcApi.DTOs
{
    public class CategoryRequestDto
    {
        [Required]
        public string Name { get; set; }

    }
}
