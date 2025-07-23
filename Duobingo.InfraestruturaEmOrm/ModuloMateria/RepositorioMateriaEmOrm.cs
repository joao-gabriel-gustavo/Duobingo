
using Duobingo.Dominio.ModuloMateria;
using Duobingo.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace Duobingo.InfraestruturaEmOrm.ModuloMateria
{
    public class RepositorioMateriaEmOrm : RepositorioBaseEmOrm<Materia>, IRepositorioMateria
    {
        private readonly duobingoDbContext contexto;

        public RepositorioMateriaEmOrm(duobingoDbContext contexto) : base(contexto)
        {
            this.contexto = contexto;
        }

        public override List<Materia> SelecionarRegistros()
        {
            return contexto.Materias
                .Include(m => m.Disciplina)
                .ToList();
        }

        public override Materia? SelecionarRegistroPorId(Guid idRegistro)
        {
            return contexto.Materias
                .Include(m => m.Disciplina)
                .FirstOrDefault(m => m.Id == idRegistro);
        }
    }
}
