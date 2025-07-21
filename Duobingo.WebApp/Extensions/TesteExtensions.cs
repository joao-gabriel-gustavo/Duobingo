using Duobingo.Dominio.ModuloDisciplina;
using Duobingo.Dominio.ModuloMateria;
using Duobingo.Dominio.ModuloTeste;
using Duobingo.WebApp.Model;

namespace Duobingo.WebApp.Extensions
{
    public static class TesteExtensions
    {

        public static DetalhesTesteViewModel ParaDetalhesVM(this Teste teste)
        {
            return new DetalhesTesteViewModel(
                teste.Id,
                teste.Titulo,
                teste.Serie,
                teste.Disciplina,
                teste.Materia
                );
        }
        public static Teste ParaEntidade(this FormularioTesteViewModel formularioVM,List<Disciplina> disciplinas)
        {
            Disciplina? disciplinaSelecionada = null;

            foreach (var d in disciplinas)
            {
                if (d.Id.Equals(formularioVM.DisciplinaId))
                {
                    disciplinaSelecionada = d;
                    break;
                }
            }

            return new Teste(
                formularioVM.Titulo,
                disciplinaSelecionada,
                formularioVM.Serie
            );
        }

    }
}
