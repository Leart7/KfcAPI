namespace KfcApi.Models.AbstractModelClasses
{
    public class CategoriesAbstractClass : BaseModel
    {
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
