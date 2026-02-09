using ProductApi.Entities;
using System.Linq.Expressions;

namespace ProductApi.DAL.Repositories.Abstract
{
    public interface ICategoryRepository
    {
        public Task<Category> GetCategoryAsync(Expression<Func<Category, bool>> filter);
        public Task<List<Category>> GetCategoriesAsync(Expression<Func<Category, bool>> filter=null);
        public Task AddAsync(Category category);
        public void Update(Category category);
        public void Remove(Category category);
        public Task SaveChangesAsync();

    }
}
