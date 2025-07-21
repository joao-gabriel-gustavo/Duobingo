using Duobingo.Dominio.ModuloQuestoes;
using Duobingo.WebApp.Model;

namespace Duobingo.WebApp.Extensions
{
    public static class QuestoesExtensions
    {
        public static DetalhesQuestaoViewModel ParaDetalhesVM(this Questoes questao)
        {
            return new DetalhesQuestaoViewModel(
                questao.Id,
                questao.Enunciado,
                questao.Materia,
                questao.Alternativas
            );
        }

        public static Questoes ParaDominio(this FormularioQuestoesViewModel viewModel, Questoes? questaoExistente = null)
        {
            var questao = questaoExistente ?? new Questoes();
            
            if (questaoExistente == null)
            {
                questao.Id = Guid.NewGuid();
            }

            questao.Enunciado = viewModel.Enunciado;
            questao.MateriaId = viewModel.MateriaId;

            // Limpa alternativas existentes se for edição
            if (questaoExistente != null)
            {
                questao.Alternativas.Clear();
            }

            // Adiciona as alternativas do view model
            foreach (var altVM in viewModel.Alternativas.Where(a => !string.IsNullOrWhiteSpace(a.Texto)))
            {
                var alternativa = new Alternativa(altVM.Texto, altVM.EhCorreta)
                {
                    Id = altVM.Id,
                    QuestaoId = questao.Id,
                    Questao = questao
                };
                questao.Alternativas.Add(alternativa);
            }

            return questao;
        }
    }
} 