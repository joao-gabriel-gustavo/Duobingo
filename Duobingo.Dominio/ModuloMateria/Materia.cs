
using Duobingo.Dominio.Compartilhado;

namespace Duobingo.Dominio.ModuloMateria
{
    public class Materia : EntidadeBase<Materia>
    {
        public string Nome { get; set; }
        public Disciplina Disciplina { get; set; }
        public string Serie { get; set; }
        public override void AtualizarRegistro(Materia registroEditado)
        {
            Id = Guid.NewGuid();
            Nome = registroEditado.Nome;
            Disciplina = registroEditado.Disciplina;
            Serie = registroEditado.Serie;
        }
    }
}
