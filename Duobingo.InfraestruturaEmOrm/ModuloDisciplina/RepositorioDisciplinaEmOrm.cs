

using Duobingo.Dominio.ModuloDisciplina;
using Duobingo.Infraestrutura.Orm.Compartilhado;
using DuoBingo.Dominio.ModuloDisciplina;

namespace Duobingo.InfraestruturaEmOrm.ModuloDisciplina
{
    public class RepositorioDisciplinaEmOrm : RepositorioBaseEmOrm<Disciplina>, IRepositorioDisciplina
    {
        public RepositorioDisciplinaEmOrm(duobingoDbContext contexto) : base(contexto)
        {
        }
    }
}
