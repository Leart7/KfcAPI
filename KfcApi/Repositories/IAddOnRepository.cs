using KfcApi.Models.AbstractModelClasses;

namespace KfcApi.Repositories
{
    public interface IAddOnRepository<T> where T : AddOnsAbstractClass
    {
        Task<List<T>> GetAllAddOns();
        Task<T> CreateAddOn(T entity);
        Task<T?> UpdateAddOn(int id, T entity);
        Task<T?> DeleteAddOn(int id);
    }
}
