
using Duobingo.Dominio.ModuloDisciplina;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Duobingo.Infraestrutura.Orm.ModuloDisciplina
{
    public class MapeadorDisciplinaEmOrm : IEntityTypeConfiguration<Disciplina>
    {
        public void Configure(EntityTypeBuilder<Disciplina> builder)
        {
            builder.Property(t => t.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(t => t.Materias)
                .WithOne(m => m.Disciplina);
        }
    }
}