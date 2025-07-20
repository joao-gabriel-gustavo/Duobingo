using Duobingo.Dominio.ModuloTeste;
using Duobingo.Infraestrutura.Orm.ModuloTeste;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Duobingo.Infraestrutura.Orm.Compartilhado
{
    public class duobingoDbContext : DbContext
    {
        public DbSet<Teste> Testes { get; set; }
        public duobingoDbContext(DbContextOptions<duobingoDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(duobingoDbContext).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            modelBuilder.ApplyConfiguration(new MapeadorTesteEmOrm());
            base.OnModelCreating(modelBuilder);
        }
    }
}