using KfcApi.Contexts;
using KfcApi.Models.AbstractModelClasses;
using Microsoft.EntityFrameworkCore;

namespace KfcApi.Repositories
{
    public class AddOnRepository<T> : IAddOnRepository<T> where T : AddOnsAbstractClass
    {
        private readonly ApplicationDbContext _db;

        public AddOnRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<T> CreateAddOn(T entity)
        {
            await _db.Set<T>().AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<T?> DeleteAddOn(int id)
        {
            var addOn = await _db.Set<T>().FirstOrDefaultAsync(a => a.Id == id);
            if(addOn == null)
            {
                return null;
            }

            _db.Set<T>().Remove(addOn);
            await _db.SaveChangesAsync();
            return addOn;
        }

        public async Task<List<T>> GetAllAddOns()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public async Task<T?> UpdateAddOn(int id, T entity)
        {
            var addOn = await _db.Set<T>().FirstOrDefaultAsync(a => a.Id == id);
            if (addOn == null)
            {
                return null;
            }
           
            addOn.Name = entity.Name;
            addOn.Updated_at = DateTime.Now;

            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
