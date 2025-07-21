
    using Duobingo.Dominio.ModuloMateria;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    namespace Duobingo.Infraestrutura.Orm.ModuloMateria
    {
        public class MapeadorMateriaEmOrm : IEntityTypeConfiguration<Materia>
        {
            public void Configure(EntityTypeBuilder<Materia> builder)
            {
                 builder.Property(t => t.Id)
                    .IsRequired()
                    .ValueGeneratedNever();

                 builder.Property(t => t.Nome)
                    .IsRequired()
                    .HasMaxLength(100);

                 builder.Property(t => t.Serie)
                    .IsRequired()
                    .HasMaxLength(50);

                 builder.HasOne(t => t.Disciplina)
                    .WithMany(d => d.Materias);

                builder.HasMany(t => t.Questoes)
                 .WithOne(q => q.Materia);
            }
        }
    }