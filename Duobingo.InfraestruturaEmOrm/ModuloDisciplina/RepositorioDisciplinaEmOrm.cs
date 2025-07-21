using Duobingo.Dominio.ModuloDisciplina;
using Duobingo.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace Duobingo.InfraestruturaEmOrm.ModuloDisciplina
{
    public class RepositorioDisciplinaEmOrm
        : RepositorioBaseEmOrm<Disciplina>, IRepositorioDisciplina
    {
        public RepositorioDisciplinaEmOrm(duobingoDbContext contexto)
            : base(contexto)
        {
        }

        public override Disciplina? SelecionarRegistroPorId(Guid idRegistro)
        {
            return registros
                .Include(d => d.Materias)
                .FirstOrDefault(x => x.Id.Equals(idRegistro));
        }

        public override List<Disciplina> SelecionarRegistros()
        {
            return registros.ToList();
        }
    }
}