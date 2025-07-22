using Duobingo.Dominio.ModuloDisciplina;
using Duobingo.Dominio.ModuloMateria;
using Duobingo.WebApp.Model;
using Microsoft.AspNetCore.Mvc;

namespace Duobingo.WebApp.Extensions
{
    public static class MateriaExtensions
    {
        public static Materia ParaEntidade(this FormularioMateriaViewModel formVm, List<Disciplina> disciplinas)
        {
            var disciplina = disciplinas.FirstOrDefault(d => d.Id.Equals(formVm.DisciplinaId));
            return new Materia(formVm.Nome, formVm.Serie, disciplina!);
        }

        public static DetalhesMateriaViewModel ParaDetalhes(this Materia materia)
        {
            return new DetalhesMateriaViewModel(materia.Id, materia.Nome, materia.Serie, materia.Disciplina.Nome);
        }
    }

}
