using Duobingo.Dominio.ModuloQuestoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Duobingo.InfraestruturaEmOrm.ModuloQuestoes
{
    public class MapeadorAlternativaEmOrm : IEntityTypeConfiguration<Alternativa>
    {
        public void Configure(EntityTypeBuilder<Alternativa> builder)
        {
            builder.Property(a => a.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(a => a.Texto)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(a => a.EhCorreta)
                .IsRequired();

            builder.Property(a => a.QuestaoId)
                .IsRequired();

            builder.HasOne(a => a.Questao)
                .WithMany(q => q.Alternativas)
                .HasForeignKey(a => a.QuestaoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
} 