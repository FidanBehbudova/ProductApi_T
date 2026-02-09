using Microsoft.EntityFrameworkCore;
using ProductApi.DAL.Repositories.Abstract;
using ProductApi.Entities;
using System.Linq;
using System.Linq.Expressions;

namespace ProductApi.DAL.Repositories.Concrete.EntityFramework
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public async Task<Product> GetProductAsync(Expression<Func<Product, bool>> filter)
        {
            return await _context.Products.FirstOrDefaultAsync(filter);
        }

        public async Task<List<Product>> GetProductsAsync(Expression<Func<Product, bool>> filter = null)
        {
            return filter == null
                   ? await _context.Products.ToListAsync()
                   : await _context.Products.Where(filter).ToListAsync();
        }

        public void Remove(Product product)
        {
            _context.Products.Remove(product);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }
    }
}
