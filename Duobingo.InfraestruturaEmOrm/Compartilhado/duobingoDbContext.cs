using Duobingo.Dominio.ModuloMateria;
using Duobingo.Dominio.ModuloTeste;
using Duobingo.Dominio.ModuloQuestoes;
using Duobingo.Infraestrutura.Orm.ModuloMateria;
using Duobingo.Infraestrutura.Orm.ModuloTeste;
using Duobingo.InfraestruturaEmOrm.ModuloQuestoes;
using Microsoft.EntityFrameworkCore;
using Duobingo.Dominio.ModuloDisciplina;

namespace Duobingo.Infraestrutura.Orm.Compartilhado
{
    public class duobingoDbContext : DbContext
    {
        public DbSet<Teste> Testes { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Questoes> Questoes { get; set; }
        public DbSet<Alternativa> Alternativas { get; set; }
        public duobingoDbContext(DbContextOptions<duobingoDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(duobingoDbContext).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            modelBuilder.ApplyConfiguration(new MapeadorTesteEmOrm());
            modelBuilder.ApplyConfiguration(new MapeadorMateriaEmOrm());
            modelBuilder.ApplyConfiguration(new MapeadorQuestoesEmOrm());
            modelBuilder.ApplyConfiguration(new MapeadorAlternativaEmOrm());
            base.OnModelCreating(modelBuilder);
        }
    }
}