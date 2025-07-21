

using Duobingo.Dominio.ModuloMateria;
using Duobingo.Infraestrutura.Orm.Compartilhado;

namespace Duobingo.InfraestruturaEmOrm.ModuloMateria
{
    public class RepositorioMateriaEmOrm : RepositorioBaseEmOrm<Materia>, IRepositorioMateria
    {
        public RepositorioMateriaEmOrm(duobingoDbContext contexto) : base(contexto)
        {
        }
    }
}
