using Duobingo.Dominio.Compartilhado;

namespace Duobingo.Dominio.ModuloDisciplina;

public class Disciplina : EntidadeBase<Disciplina>
{
    public string Nome { get; set; }

    public Disciplina()
    {
    }

    public Disciplina(string nome)
    {
        Id = Guid.NewGuid();
        Nome = nome;
    }

    public override void AtualizarRegistro(Disciplina registroEditado)
    {
        Nome = registroEditado.Nome;
    }
}