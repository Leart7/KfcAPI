using KfcApi.Models.AbstractModelClasses;

namespace KfcApi.Repositories
{
    public interface ICategoryRepository<T> where T : CategoriesAbstractClass
    {
        Task<List<T>> GetAllCategories();
        Task<T> CreateCategory(T entity);
        Task<T?> UpdateCategory(int id, T entity);
        Task<T?> DeleteCategory(int id);
    }
}
