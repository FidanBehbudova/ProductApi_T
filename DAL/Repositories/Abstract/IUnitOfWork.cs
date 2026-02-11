namespace ProductApi.DAL.Repositories.Abstract
{
    public interface IUnitOfWork
    {
        public IProductRepository Products { get; }
        public ICategoryRepository Categories { get; }
        public Task SaveChangesAsync();
    }
}
