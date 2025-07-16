using Microsoft.EntityFrameworkCore;

namespace Duobingo.Infraestrutura.Orm.Compartilhado
{
    public class duobingoDbContext : DbContext
    {
        public duobingoDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(duobingoDbContext).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}