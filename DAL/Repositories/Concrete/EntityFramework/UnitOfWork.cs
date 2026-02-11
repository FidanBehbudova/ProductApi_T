using Microsoft.EntityFrameworkCore;
using ProductApi.DAL.Repositories.Abstract;

namespace ProductApi.DAL.Repositories.Concrete.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IProductRepository Products { get; }
        public ICategoryRepository Categories { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Products = new ProductRepository(_context);
            Categories = new CategoryRepository(_context);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }


}
