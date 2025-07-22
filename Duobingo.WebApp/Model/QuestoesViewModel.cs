using Duobingo.Dominio.ModuloMateria;
using Duobingo.Dominio.ModuloQuestoes;
using Duobingo.WebApp.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Duobingo.WebApp.Model
{
    public class FormularioQuestoesViewModel
    {
        public Guid Id { get; set; }
        public string Enunciado { get; set; }
        public Guid MateriaId { get; set; } 
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
        public string Enunciado { get; set; }
        public DetalhesMateriaTesteViewModel Materia { get; set; }
        public List<DetalhesAlternativaViewModel> Alternativas { get; set; }
        public string RespostaCorreta { get; set; }

        public DetalhesQuestaoViewModel(Guid id, string enunciado, Materia materia, List<Alternativa> alternativas)
        {
            Id = id;
            Enunciado = enunciado;
            Materia = new DetalhesMateriaTesteViewModel(materia.Id, materia.Nome);
            Alternativas = alternativas.Select(a => new DetalhesAlternativaViewModel(a.Id, a.Texto, a.EhCorreta)).ToList();
            RespostaCorreta = alternativas.FirstOrDefault(a => a.EhCorreta)?.Texto ?? "Não definida";
        }
    }

    public class DetalhesAlternativaViewModel
    {
        public Guid Id { get; set; }
        public string Texto { get; set; }
        public bool EhCorreta { get; set; }

        public DetalhesAlternativaViewModel()
        {
            Id = Guid.NewGuid();
        }

        public DetalhesAlternativaViewModel(Guid id, string texto, bool ehCorreta) : this()
        {
            Id = id;
            Texto = texto;
            EhCorreta = ehCorreta;
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

        public EditarQuestaoViewModel(Questoes questao, List<Materia> materias) : this()
        {
            Id = questao.Id;
            Enunciado = questao.Enunciado;
            MateriaId = questao.Materia?.Id ?? Guid.Empty;
            Materia = questao.Materia;
            
            foreach (var a in questao.Alternativas)
            {
                Alternativas.Add(new DetalhesAlternativaViewModel(a.Id, a.Texto, a.EhCorreta));
            }

            foreach (var m in materias)
            {
                var materiaVM = new DetalhesMateriaTesteViewModel(m.Id, m.Nome);
                MateriasDisponiveis.Add(materiaVM);
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
            Alternativas = questao.Alternativas.Select(a => new DetalhesAlternativaViewModel(a.Id, a.Texto, a.EhCorreta)).ToList();
        }
    }
} 