using System.ComponentModel.DataAnnotations;

namespace KfcApi.DTOs
{
    public class AddOnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }

    public class AddOnRequestDto
    {
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
