
using System.Runtime.CompilerServices;
using Duobingo.Dominio.Compartilhado;
using Duobingo.Dominio.ModuloDisciplina;
using Duobingo.Dominio.ModuloMateria;
using Duobingo.Dominio.ModuloQuestoes;

namespace Duobingo.Dominio.ModuloTeste
{
    public class Teste : EntidadeBase<Teste>
    {

        public string Titulo { get; set; }
        public Disciplina Disciplina { get; set; }
        public List<Materia> Materia { get; set; }
        public List<Questoes> Questoes { get; set; }
        public string Serie { get; set; }   
        public bool EhRecuperacao { get; set; }
        public Teste ()
        {

        }
        public Teste (string titulo, Disciplina disciplina, string serie, bool ehRecuperacao)
        {
            Id = Guid.NewGuid();
            Titulo = string.Empty;
            Disciplina = disciplina;
            Materia =  new List<Materia>();
            Questoes = new List<Questoes>();
            Serie = serie;
            EhRecuperacao = ehRecuperacao;
        }

        public Teste(string titulo, Disciplina disciplina, string serie) : this()
        {
            Id = Guid.NewGuid();
            Titulo = string.Empty;
            Disciplina = disciplina;
            Materia = new List<Materia>();
            Questoes = new List<Questoes>();
            Serie = serie;
        }
        public override void AtualizarRegistro(Teste registroEditado)
        {
            Id = registroEditado.Id;
            Titulo = registroEditado.Titulo;
            Disciplina = registroEditado.Disciplina;
            Materia = registroEditado.Materia;
            Serie = registroEditado.Serie;
            EhRecuperacao = registroEditado.EhRecuperacao;
        }
    }
}
