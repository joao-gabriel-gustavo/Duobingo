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
    }
}
