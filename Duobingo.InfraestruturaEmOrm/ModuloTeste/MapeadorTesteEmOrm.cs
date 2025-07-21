
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

            builder.Property(t => t.Serie)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(t => t.Disciplina);

            builder.HasMany(t => t.Materia);
        }
    }
}