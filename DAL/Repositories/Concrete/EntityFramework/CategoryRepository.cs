using Microsoft.EntityFrameworkCore;
using ProductApi.DAL.Repositories.Abstract;
using ProductApi.Entities;
using System.Linq.Expressions;

namespace ProductApi.DAL.Repositories.Concrete.EntityFramework
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public async Task<List<Category>> GetCategoriesAsync(Expression<Func<Category, bool>> filter = null)
        {
            return filter == null
                ?  await _context.Categories.ToListAsync()
                : await _context.Categories.Where(filter).ToListAsync();

        }

        public async Task<Category> GetCategoryAsync(Expression<Func<Category, bool>> filter)
        {
           return await _context.Categories.FirstOrDefaultAsync(filter);
        }

        public void Remove(Category category)
        {
           _context.Categories.Remove(category);
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }

        public void Update(Category category)
        {
           _context.Categories.Update(category);
        }
    }
}
