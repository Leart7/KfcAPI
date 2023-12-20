using System.ComponentModel.DataAnnotations;

namespace KfcApi.Models.AbstractModelClasses
{
    public abstract class BaseModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime Created_at { get; set; } = DateTime.Now;
        public DateTime Updated_at { get; set; } = DateTime.Now;

    }
}
