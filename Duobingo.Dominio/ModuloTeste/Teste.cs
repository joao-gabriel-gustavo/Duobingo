
using System.Runtime.CompilerServices;
using Duobingo.Dominio.Compartilhado;
using Duobingo.Dominio.ModuloMateria;

namespace Duobingo.Dominio.ModuloTeste
{
    public class Teste : EntidadeBase<Teste>
    {

        public string Titulo { get; set; }
        public Disciplina Disciplina { get; set; }
        public List<Materia> Materia { get; set; }
        public string Serie { get; set; }   

        public Teste (string titulo, Disciplina disciplina, string serie)
        {
            Id = Guid.NewGuid();
            Titulo = string.Empty;
            Disciplina = disciplina;
            Materia =  new List<Materia>();
            Serie = serie;

        }
        public override void AtualizarRegistro(Teste registroEditado)
        {
            Id = registroEditado.Id;
            Titulo = registroEditado.Titulo;
            Disciplina = registroEditado.Disciplina;
            Materia = registroEditado.Materia;
            Serie = registroEditado.Serie;
        }
    }
}
