
using Duobingo.Dominio.Compartilhado;
using Duobingo.Dominio.ModuloMateria;
using Duobingo.Dominio.ModuloTeste;

namespace Duobingo.Dominio.ModuloQuestoes
{
    public class Alternativa : EntidadeBase<Alternativa>
    {
        public string Texto { get; set; }
        public bool EhCorreta { get; set; }
        public Guid QuestaoId { get; set; }
        public Questoes Questao { get; set; }

        public Alternativa()
        {
            Id = Guid.NewGuid();
        }

        public Alternativa(string texto, bool ehCorreta = false) : this()
        {
            Texto = texto;
            EhCorreta = ehCorreta;
        }

        public override void AtualizarRegistro(Alternativa registroEditado)
        {
            Texto = registroEditado.Texto;
            EhCorreta = registroEditado.EhCorreta;
            QuestaoId = registroEditado.QuestaoId;
        }
    }

    public class Questoes : EntidadeBase<Questoes>
    {
        public Materia Materia { get; set; }
        
        public string Enunciado { get; set; }
        public List<Alternativa> Alternativas { get; set; }

        public List<Teste> Testes { get; set; }
        public Questoes()
        {
            Id = Guid.NewGuid();
            Alternativas = new List<Alternativa>();
        }

        public Questoes(Materia materia, string enunciado) : this()
        {
            Materia = materia;
           
            Enunciado = enunciado;
        }

        public void AdicionarAlternativa(string texto, bool ehCorreta = false)
        {
            if (Alternativas.Count >= 4)
                throw new InvalidOperationException("Uma questão pode ter no máximo 4 alternativas.");

            if (ehCorreta && TemAlternativaCorreta())
                throw new InvalidOperationException("Uma questão pode ter apenas uma alternativa correta.");

            var alternativa = new Alternativa(texto, ehCorreta)
            {
                QuestaoId = this.Id,
                Questao = this
            };

            Alternativas.Add(alternativa);
        }

        public void RemoverAlternativa(int indice)
        {
            if (indice < 0 || indice >= Alternativas.Count)
                throw new ArgumentOutOfRangeException("Índice da alternativa inválido.");

            if (Alternativas.Count <= 2)
                throw new InvalidOperationException("Uma questão deve ter no mínimo 2 alternativas.");

            Alternativas.RemoveAt(indice);
        }

        public void DefinirAlternativaCorreta(int indice)
        {
            if (indice < 0 || indice >= Alternativas.Count)
                throw new ArgumentOutOfRangeException("Índice da alternativa inválido.");

            foreach (var alternativa in Alternativas)
                alternativa.EhCorreta = false;

            Alternativas[indice].EhCorreta = true;
        }

        public bool TemAlternativaCorreta()
        {
            return Alternativas.Any(a => a.EhCorreta);
        }

        public string ObterRespostaCorreta()
        {
            var alternativaCorreta = Alternativas.FirstOrDefault(a => a.EhCorreta);
            return alternativaCorreta?.Texto ?? "Nenhuma resposta correta definida";
        }

        public bool EhValida()
        {
            return Alternativas.Count >= 2 && 
                   Alternativas.Count <= 4 && 
                   TemAlternativaCorreta() &&
                   !string.IsNullOrWhiteSpace(Enunciado) &&
                   Materia != null;
        }

        public override void AtualizarRegistro(Questoes registroEditado)
        {
            Materia = registroEditado.Materia;
            Enunciado = registroEditado.Enunciado;
            
            Alternativas.Clear();
            foreach (var alternativa in registroEditado.Alternativas)
            {
                alternativa.QuestaoId = this.Id;
                alternativa.Questao = this;
                Alternativas.Add(alternativa);
            }
        }
    }
}
