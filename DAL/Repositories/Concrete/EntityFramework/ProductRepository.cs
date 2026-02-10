using Microsoft.EntityFrameworkCore;
using ProductApi.Core.DAL.Repositories.Concrete.EntityFramework;
using ProductApi.DAL.Repositories.Abstract;
using ProductApi.Entities;
using System.Linq;
using System.Linq.Expressions;

namespace ProductApi.DAL.Repositories.Concrete.EntityFramework
{
    public class ProductRepository : Repository<Product, AppDbContext>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}
