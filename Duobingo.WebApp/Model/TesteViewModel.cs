using Duobingo.Dominio.ModuloDisciplina;
using Duobingo.Dominio.ModuloMateria;
using Duobingo.Dominio.ModuloTeste;
using Duobingo.WebApp.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Security.Cryptography.X509Certificates;

namespace Duobingo.WebApp.Model
{
    public class FormularioTesteViewModel
    {
        public string Titulo { get; set; }
        public string Serie { get; set; }
        public Guid DisciplinaId { get; set; }
        public List<Guid> MateriasIds { get; set; }
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
        public List<DetalhesMateriaViewModel> Materias { get; set; }

        public DetalhesDisciplinaViewModel Disciplina { get; set; }

        public DetalhesTesteViewModel(Guid Id, string Titulo, string serie, Disciplina disciplina, List<Materia> materias) 
        {
           Disciplina = new DetalhesDisciplinaViewModel(disciplina.Id, disciplina.Nome);
           Serie = serie;

            foreach (var materia in materias)
            {
                var DetalhesMateriaViewModel = new DetalhesMateriaViewModel(materia.Id, materia.Nome);
                Materias.Add(DetalhesMateriaViewModel);
            }
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

    public class DetalhesMateriaViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public DetalhesMateriaViewModel(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}
