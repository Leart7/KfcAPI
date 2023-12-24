using KfcApi.Models.AbstractModelClasses;
using System.ComponentModel.DataAnnotations.Schema;

namespace KfcApi.Models
{
    public class OrderUser : BaseModel
    {
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}
