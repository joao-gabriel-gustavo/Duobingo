using Duobingo.Dominio.ModuloMateria;
using Duobingo.Dominio.ModuloQuestoes;
using Duobingo.WebApp.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Duobingo.WebApp.Model
{
    public class FormularioQuestoesViewModel
    {
        public Guid Id { get; set; }
        public string Enunciado { get; set; } = string.Empty;
        public Guid SelectedMateriaId { get; set; }
        public Materia? Materia { get; set; }
        public List<DetalhesAlternativaViewModel> Alternativas { get; set; }
        public List<DetalhesMateriaTesteViewModel> MateriasDisponiveis { get; set; }

        public FormularioQuestoesViewModel()
        {
            Alternativas = new List<DetalhesAlternativaViewModel>();
            MateriasDisponiveis = new List<DetalhesMateriaTesteViewModel>();
        }
    }

    public class VisualizarQuestoesViewModel
    {
        public List<DetalhesQuestaoViewModel> Registros { get; set; }

        public VisualizarQuestoesViewModel(List<Questoes> questoes)
        {
            Registros = new List<DetalhesQuestaoViewModel>();

            foreach (var q in questoes)
                Registros.Add(q.ParaDetalhesVM());
        }
    }

    public class DetalhesQuestaoViewModel
    {
        public Guid Id { get; set; }
        public string Enunciado { get; set; } = string.Empty;
        public DetalhesMateriaTesteViewModel Materia { get; set; } = null!;
        public List<DetalhesAlternativaViewModel> Alternativas { get; set; }
        public string RespostaCorreta { get; set; } = string.Empty;

        public DetalhesQuestaoViewModel()
        {
            Alternativas = new List<DetalhesAlternativaViewModel>();
        }

        public DetalhesQuestaoViewModel(Guid id, string enunciado, Materia materia, List<Alternativa> alternativas) : this()
        {
            Id = id;
            Enunciado = enunciado;
            Materia = new DetalhesMateriaTesteViewModel(materia.Id, materia.Nome);
            Alternativas = alternativas.Select(a => new DetalhesAlternativaViewModel(a.Id, a.Letra, a.Resposta, a.Correta)).ToList();
            RespostaCorreta = alternativas.FirstOrDefault(a => a.Correta)?.Resposta ?? "Não definida";
        }

        public DetalhesQuestaoViewModel(Questoes questao) : this()
        {
            Id = questao.Id;
            Enunciado = questao.Enunciado;
            Materia = new DetalhesMateriaTesteViewModel(questao.Materia.Id, questao.Materia.Nome);

            var alternativas = questao.Alternativas.ToList();
            Alternativas = alternativas.Select(a => new DetalhesAlternativaViewModel(a.Id, a.Letra, a.Resposta, a.Correta)).ToList();
            RespostaCorreta = alternativas.FirstOrDefault(a => a.Correta)?.Resposta ?? "Não definida";
        }
    }

    public class DetalhesAlternativaViewModel
    {
        public Guid Id { get; set; }
        public string Letra { get; set; } = string.Empty;
        public string Resposta { get; set; } = string.Empty;
        public bool Correta { get; set; }

        public DetalhesAlternativaViewModel() { }

        public DetalhesAlternativaViewModel(Guid id, string letra, string resposta, bool correta) : this()
        {
            Id = id;
            Letra = letra;
            Resposta = resposta;
            Correta = correta;
        }
    }

    public class CadastrarQuestaoViewModel : FormularioQuestoesViewModel
    {
        public CadastrarQuestaoViewModel()
        {
            // Adiciona duas alternativas por padrão
            Alternativas.Add(new DetalhesAlternativaViewModel());
            Alternativas.Add(new DetalhesAlternativaViewModel());
        }

        public CadastrarQuestaoViewModel(List<Materia> materias) : this()
        {
            foreach (var m in materias)
            {
                var materiaVM = new DetalhesMateriaTesteViewModel(m.Id, m.Nome);
                MateriasDisponiveis.Add(materiaVM);
            }
        }
    }

    public class EditarQuestaoViewModel : FormularioQuestoesViewModel
    {
        public EditarQuestaoViewModel()
        {
        }

        public EditarQuestaoViewModel(Questoes questao) : this()
        {
            Id = questao.Id;
            Materia = questao.Materia;
            SelectedMateriaId = questao.Materia.Id;
            Enunciado = questao.Enunciado;

            Alternativas = questao.Alternativas.Select(a => new DetalhesAlternativaViewModel(a.Id, a.Letra, a.Resposta, a.Correta)).ToList();

            // Adicionar alternativas vazias para completar 4
            while (Alternativas.Count < 4)
            {
                Alternativas.Add(new DetalhesAlternativaViewModel());
            }
        }
    }

    public class ExcluirQuestaoViewModel
    {
        public Guid Id { get; set; }
        public string Enunciado { get; set; }
        public string NomeMateria { get; set; }
        public string RespostaCorreta { get; set; }
        public List<DetalhesAlternativaViewModel> Alternativas { get; set; }

        public ExcluirQuestaoViewModel(Questoes questao)
        {
            Id = questao.Id;
            Enunciado = questao.Enunciado;
            NomeMateria = questao.Materia?.Nome ?? "Não informada";
            RespostaCorreta = questao.ObterRespostaCorreta();
            Alternativas = questao.Alternativas.Select(a => new DetalhesAlternativaViewModel(a.Id, a.Letra, a.Resposta, a.Correta)).ToList();
        }
    }
} 