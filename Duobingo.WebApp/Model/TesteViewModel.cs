using Duobingo.Dominio.ModuloDisciplina;
using Duobingo.Dominio.ModuloMateria;
using Duobingo.Dominio.ModuloTeste;
using Duobingo.WebApp.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Security.Cryptography.X509Certificates;

namespace Duobingo.WebApp.Model
{
    public class FormularioTesteViewModel
    {
        public string Titulo { get; set; }
        public string Serie { get; set; }
        public int QuantidadeQuestoes { get; set; }
        public Guid DisciplinaId { get; set; }
        public Guid MateriaId { get; set; }
        public List<string> SeriesDisponiveis { get; set; }
        public bool EhRecuperacao { get; set; }
        public List<DetalhesMateriaTesteViewModel>? MateriasDisponiveis { get; set; }
        public List<DetalhesDisciplinaViewModel> DisciplinasDisponiveis { get; set; }

    }

    public class CadastrarTesteViewModel : FormularioTesteViewModel
    {
        public CadastrarTesteViewModel()
        {
            MateriasDisponiveis = new List<DetalhesMateriaViewModel>();
            DisciplinasDisponiveis = new List<DetalhesDisciplinaViewModel>();
        }

        public CadastrarTesteViewModel(List<Materia> materias, List<Disciplina> disciplinas) : this()
        {
            foreach (var m in materias)
            {
                var selecionarVM = new DetalhesMateriaViewModel(m.Id, m.Nome);

                MateriasDisponiveis?.Add(selecionarVM);
            }

            foreach(var d in disciplinas)
            {
                var selecionarVM = new DetalhesDisciplinaViewModel(d.Id, d.Nome);

                DisciplinasDisponiveis?.Add(selecionarVM);
            }

        }
    }


    public class VisualizarTestesViewModel
    {
        public List<DetalhesTesteViewModel> Registros { get; set; }

        public VisualizarTestesViewModel(List<Teste> testes)
        {
            Registros = new List<DetalhesTesteViewModel>();

            foreach (var g in testes)
                Registros.Add(g.ParaDetalhesVM());
        }
    }


    public class DetalhesTesteViewModel
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Serie { get; set; }
        public int QuantidadeQuestoes { get; set; }
        public DetalhesMateriaTesteViewModel Materia { get; set; }
        public DetalhesDisciplinaViewModel Disciplina { get; set; }

        public DetalhesTesteViewModel(Guid Id, string Titulo, string serie, Disciplina disciplina, Materia materia)
        {
            Disciplina = new DetalhesDisciplinaViewModel(disciplina.Id, disciplina.Nome);
            Serie = serie;
            Materia = new  DetalhesMateriaTesteViewModel(materia.Id, materia.Nome);
        }
    }

    public class DetalhesDisciplinaViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public DetalhesDisciplinaViewModel(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }

    public class DetalhesMateriaTesteViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }


        public DetalhesMateriaTesteViewModel(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }

    }


}

