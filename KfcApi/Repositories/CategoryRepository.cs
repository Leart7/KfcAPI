using KfcApi.Contexts;
using KfcApi.Models.AbstractModelClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KfcApi.Repositories
{
    public class CategoryRepository<T> : ICategoryRepository<T> where T : CategoriesAbstractClass
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<T>> GetAllCategories()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public async Task<T> CreateCategory(T entity)
        {
            await _db.Set<T>().AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<T?> UpdateCategory(int id, T entity)
        {
            var existingEntity = await _db.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
            if (existingEntity == null)
            {
                return null;
            }

            existingEntity.Name = entity.Name;
            existingEntity.Updated_at = DateTime.Now;

            await _db.SaveChangesAsync();
            return existingEntity;
        }

        public async Task<T?> DeleteCategory(int id)
        {
            var entity = await _db.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
            if (entity == null)
            {
                return null;
            }

            _db.Set<T>().Remove(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
