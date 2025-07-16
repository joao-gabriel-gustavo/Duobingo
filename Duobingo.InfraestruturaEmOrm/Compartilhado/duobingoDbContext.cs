using eAgenda.Dominio.ModuloContato;
using eAgenda.Dominio.ModuloCompromisso;
using Microsoft.EntityFrameworkCore;

namespace eAgenda.Infraestrutura.Orm.Compartilhado
{
    public class duobingoDbContexto : DbContext
    {
        public duobingoDbContexto(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(duobingoDbContexto).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}