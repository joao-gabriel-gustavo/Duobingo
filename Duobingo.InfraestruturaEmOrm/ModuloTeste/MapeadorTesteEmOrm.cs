
using Duobingo.Dominio.ModuloTeste;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Duobingo.Infraestrutura.Orm.ModuloTeste
{
    public class MapeadorTesteEmOrm : IEntityTypeConfiguration<Teste>
    {
        public void Configure(EntityTypeBuilder<Teste> builder)
        {

            builder.Property(t => t.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(t => t.Titulo)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(t => t.Disciplina)
                .WithMany();

            builder.HasOne(t => t.Materia)
                .WithMany(m => m.Testes);

            builder.HasMany(t => t.Questoes)
                .WithOne();

            builder.Property(t => t.Serie)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.QuantidadeQuestoes)
                .IsRequired();

            builder.Property(t => t.EhRecuperacao)
                .IsRequired();
        }
    }
}