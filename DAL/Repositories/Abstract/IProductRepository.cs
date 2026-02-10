using ProductApi.Core.DAL.Repositories.Abstract;
using ProductApi.Entities;
using System.Linq.Expressions;

namespace ProductApi.DAL.Repositories.Abstract
{
    public interface IProductRepository:IRepository<Product>
    {
   
    }
}
