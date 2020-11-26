using Microsoft.EntityFrameworkCore;

namespace MSD.Product.Repository.Db.Context
{
    public class EntitiesContext : DbContext
    {
        public EntitiesContext(DbContextOptions<EntitiesContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
