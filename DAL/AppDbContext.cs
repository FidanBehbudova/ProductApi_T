using Microsoft.EntityFrameworkCore;
using ProductApi.Entities;

namespace ProductApi.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }

    }
}
