using Microsoft.EntityFrameworkCore;
using ProductApi.Core.DAL.Repositories.Concrete.EntityFramework;
using ProductApi.DAL.Repositories.Abstract;
using ProductApi.Entities;
using System.Linq.Expressions;

namespace ProductApi.DAL.Repositories.Concrete.EntityFramework
{
    public class CategoryRepository : Repository<Category, AppDbContext>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
