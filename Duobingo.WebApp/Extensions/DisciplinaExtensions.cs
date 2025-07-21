using Duobingo.Dominio.ModuloDisciplina;
using Duobingo.WebApp.Model;

namespace Duobingo.WebApp.Extensions
{
    public static class DisciplinaExtensions
    {
        public static Disciplina ParaEntidade(this FormularioDisciplinaViewModel formularioVM)
        {
            return new Disciplina(formularioVM.Nome);
        }

        public static DetalhesDisciplinaViewModel ParaDetalhes(this Disciplina disciplina)
        {
            return new DetalhesDisciplinaViewModel(disciplina.Id, disciplina.Nome);
        }
    }
}