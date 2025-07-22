using Duobingo.Dominio.Compartilhado;
using Duobingo.Dominio.ModuloMateria;

namespace Duobingo.Dominio.ModuloQuestoes
{
    public interface IRepositorioQuestoes : IRepositorio<Questoes>
    {
        List<Questoes> SelecionarQuestoesPorMateria(Materia materia);
        bool QuestaoEstaVinculadaATeste(Guid questaoId);
        bool ExcluirQuestao(Guid questaoId);
    }
}
