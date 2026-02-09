using ProductApi.Entities;
using System.Linq.Expressions;

namespace ProductApi.DAL.Repositories.Abstract
{
    public interface IProductRepository
    {
        public Task<Product> GetProductAsync(Expression<Func<Product, bool>> filter);
        public Task<List<Product>> GetProductsAsync(Expression<Func<Product, bool>> filter = null);
        public Task AddAsync(Product product);
        public void Update(Product product);
        public void Remove(Product product);
        public Task SaveChangesAsync();
    }
}
