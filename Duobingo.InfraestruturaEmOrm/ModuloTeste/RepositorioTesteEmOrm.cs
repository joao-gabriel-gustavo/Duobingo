

using Duobingo.Dominio.ModuloTeste;
using Duobingo.Infraestrutura.Orm.Compartilhado;

namespace Duobingo.InfraestruturaEmOrm.ModuloTeste
{
    public class RepositorioTesteEmOrm : RepositorioBaseEmOrm<Teste>, IRepositorioTeste
    {
        public RepositorioTesteEmOrm(duobingoDbContext contexto) : base(contexto)
        {
        }
    }
}
