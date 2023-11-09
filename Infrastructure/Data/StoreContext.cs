using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }
        public DbSet<Character> Champions { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuiler)
        {
            base.OnModelCreating(modelBuiler);
            modelBuiler.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
