using KfcApi.Models.AbstractModelClasses;
using System.ComponentModel.DataAnnotations.Schema;

namespace KfcApi.Models
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public bool HasMenu { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public int CategoryId { get; set; }
        public int? HomeCategoryId { get; set; }

        public Category Category { get; set; }
        public HomeCategory? HomeCategory { get; set; }


    }
}
