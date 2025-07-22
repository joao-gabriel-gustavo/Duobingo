
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
        public Materia Materia { get; set; }
        public List<Questoes> Questoes { get; set; }
        public string Serie { get; set; }   

        public int QuantidadeQuestoes { get;set; }
        public bool EhRecuperacao { get; set; }
        public Teste ()
        {

        }
        public Teste (string titulo, Disciplina disciplina, Materia materia, int quantidadeQuestoes, string serie, bool ehRecuperacao)
        {
            Id = Guid.NewGuid();
            Titulo = string.Empty;
            Disciplina = disciplina;
            Materia = materia;
            Questoes = new List<Questoes>();
            QuantidadeQuestoes = quantidadeQuestoes;
            Serie = serie;
            EhRecuperacao = ehRecuperacao;
        }

        public Teste(string titulo, Disciplina disciplina, int quantidadeQuestoes, string serie) : this()
        {
            Id = Guid.NewGuid();
            Titulo = string.Empty;
            Disciplina = disciplina;
            Questoes = new List<Questoes>();
            QuantidadeQuestoes = quantidadeQuestoes;
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
