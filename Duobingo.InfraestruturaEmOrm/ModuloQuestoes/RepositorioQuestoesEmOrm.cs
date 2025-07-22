using Duobingo.Dominio.ModuloQuestoes;
using Duobingo.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace Duobingo.InfraestruturaEmOrm.ModuloQuestoes
{
    public class RepositorioQuestoesEmOrm : RepositorioBaseEmOrm<Questoes>, IRepositorioQuestoes
    {
        private readonly duobingoDbContext contexto;

        public RepositorioQuestoesEmOrm(duobingoDbContext contexto) : base(contexto)
        {
            this.contexto = contexto;
        }

        public override List<Questoes> SelecionarRegistros()
        {
            return contexto.Questoes
                .Include(q => q.Materia)
                    .ThenInclude(m => m.Disciplina)
                .Include(q => q.Alternativas)
                .ToList();
        }

        public override Questoes? SelecionarRegistroPorId(Guid idRegistro)
        {
            return contexto.Questoes
                .Include(q => q.Materia)
                    .ThenInclude(m => m.Disciplina)
                .Include(q => q.Alternativas)
                .FirstOrDefault(q => q.Id == idRegistro);
        }

        public List<Questoes> SelecionarQuestoesPorMateria(Guid materiaId)
        {
            return contexto.Questoes
                .Include(q => q.Materia)
                    .ThenInclude(m => m.Disciplina)
                .Include(q => q.Alternativas)
                //.Where(q => q.MateriaId == materiaId)
                .ToList();
        }

        public bool QuestaoEstaVinculadaATeste(Guid questaoId)
        {
            return false;
        }

        public bool ExcluirQuestao(Guid questaoId)
        {
            if (QuestaoEstaVinculadaATeste(questaoId))
                return false;

            return ExcluirRegistro(questaoId);
        }
    }
} 