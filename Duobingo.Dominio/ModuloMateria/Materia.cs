
using Duobingo.Dominio.Compartilhado;
using Duobingo.Dominio.ModuloDisciplina;
using Duobingo.Dominio.ModuloQuestoes;
using Duobingo.Dominio.ModuloTeste;

namespace Duobingo.Dominio.ModuloMateria
{
    public class Materia : EntidadeBase<Materia>
    {
        public string Nome { get; set; }
        public Serie Serie { get; set; }
        public Disciplina Disciplina { get; set; }
        public List<Questoes> Questoes { get; set; }
        public List<Teste> Testes { get; set; }

        public Materia()
        {
            Testes = new List<Teste>();
            Questoes = new List<Questoes>();
        }

        public Materia(string nome, Serie serie, Disciplina disciplina) : this()
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Serie = serie;
            Disciplina = disciplina;
        }
        public override void AtualizarRegistro(Materia registroEditado)
        {
            Nome = registroEditado.Nome;
            Disciplina = registroEditado.Disciplina;
            Serie = registroEditado.Serie;
        }
    }
}