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
                .HasMaxLength(1000);

            builder.Property(q => q.MateriaId)
                .IsRequired();

            builder.HasOne(q => q.Materia)
                .WithMany()
                .HasForeignKey(q => q.MateriaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(q => q.Alternativas)
                .WithOne(a => a.Questao)
                .HasForeignKey(a => a.QuestaoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
} 