using Duobingo.Dominio.ModuloDisciplina;
using Duobingo.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace Duobingo.InfraestruturaEmOrm.ModuloDisciplina
{
    public class RepositorioDisciplinaEmOrm : RepositorioBaseEmOrm<Disciplina>, IRepositorioDisciplina
    {
        public RepositorioDisciplinaEmOrm(duobingoDbContext contexto): base(contexto)
        {
        }

    }
}