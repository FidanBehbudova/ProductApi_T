using Microsoft.EntityFrameworkCore;
using ProductApi.Core.DAL.Repositories.Abstract;
using System.Linq.Expressions;

namespace ProductApi.Core.DAL.Repositories.Concrete.EntityFramework
{
    public class Repository<TEntity,TContext>:IRepository<TEntity>   
        where TEntity : class,new()
        where TContext : DbContext
    {
        private readonly TContext _context;

        public Repository(TContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TEntity tentity)
        {
            await _context.Set<TEntity>().AddAsync(tentity);
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, params string[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            query = GetQuery(includes);

            return filter == null
                ? await query.ToListAsync()
                : await query.Where(filter).ToListAsync();

        }

        public async Task<List<TEntity>> GetPaginateAsync(int page, int size, Expression<Func<TEntity, bool>> filter = null, params string[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            query = GetQuery(includes);
            return filter == null
                 ? await query.Skip((page - 1) * size).Take(size).ToListAsync()
                 : await query.Where(filter).Skip((page - 1) * size).Take(size).ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, params string[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            query = GetQuery(includes);
            return await query.FirstOrDefaultAsync(filter);
        }

        public void Remove(TEntity tentity)
        {
            _context.Set<TEntity>().Remove(tentity);
            _context.SaveChanges();
        }

     

        public void Update(TEntity tentity)
        {
            _context.Set<TEntity>().Update(tentity);
        }

        public IQueryable<TEntity> GetQuery(string[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query;
        }
    }
}
