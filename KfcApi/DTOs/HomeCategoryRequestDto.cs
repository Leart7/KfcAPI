using System.ComponentModel.DataAnnotations;

namespace KfcApi.DTOs
{
    public class HomeCategoryRequestDto
    {
        [Required]
        public string Name { get; set; }

    }
}
