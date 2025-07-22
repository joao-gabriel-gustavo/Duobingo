using Duobingo.Dominio.ModuloQuestoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Duobingo.InfraestruturaEmOrm.ModuloQuestoes
{
    public class MapeadorQuestoesEmOrm : IEntityTypeConfiguration<Questoes>
    {
        public void Configure(EntityTypeBuilder<Questoes> builder)
        {
            builder.Property(q => q.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(q => q.Enunciado)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(q => q.UtilizadaEmTeste)
                .IsRequired()

            builder.HasOne(q => q.Materia)
                .WithMany(m => m.Questoes)
                .IsRequired();

            builder.HasMany(q => q.Alternativas)
                .WithOne(a => a.Questao)
        }
    }
} 