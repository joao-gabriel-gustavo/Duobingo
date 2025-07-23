
using Duobingo.Dominio.Compartilhado;
using Duobingo.Dominio.ModuloMateria;

namespace Duobingo.Dominio.ModuloQuestoes
{
    public class Alternativa : EntidadeBase<Alternativa>
    {
        public string Letra { get; set; }
        public string Resposta { get; set; }
        public bool Correta { get; set; }
        public Guid QuestaoId { get; set; }
        public Questoes Questao { get; set; }

        public Alternativa()
        {
            Id = Guid.NewGuid();
        }

        public Alternativa(string letra, string resposta, bool correta = false) : this()
        {
            Letra = letra;
            Resposta = resposta;
            Correta = correta;
        }

        public override void AtualizarRegistro(Alternativa registroEditado)
        {
            Letra = registroEditado.Letra;
            Resposta = registroEditado.Resposta;
            Correta = registroEditado.Correta;
            QuestaoId = registroEditado.QuestaoId;
        }
    }

    public class Questoes : EntidadeBase<Questoes>
    {
        public Materia Materia { get; set; }
        public string Enunciado { get; set; }
        public bool UtilizadaEmTeste { get; set; }
        public List<Alternativa> Alternativas { get; set; }

        public Questoes()
        {
            Id = Guid.NewGuid();
            Alternativas = new List<Alternativa>();
        }

        public Questoes(Materia materia, string enunciado) : this()
        {
            Materia = materia;
            Enunciado = enunciado;
            UtilizadaEmTeste = false;
        }

        public void AdicionarAlternativa(string letra, string resposta, bool correta = false)
        {
            if (Alternativas.Count >= 4)
                throw new InvalidOperationException("Uma questão pode ter no máximo 4 alternativas.");

            if (correta && TemAlternativaCorreta())
                throw new InvalidOperationException("Uma questão pode ter apenas uma alternativa correta.");

            var alternativa = new Alternativa(letra, resposta, correta)
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
                alternativa.Correta = false;

            Alternativas[indice].Correta = true;
        }

        public bool TemAlternativaCorreta()
        {
            return Alternativas.Any(a => a.Correta);
        }

        public string ObterRespostaCorreta()
        {
            var alternativaCorreta = Alternativas.FirstOrDefault(a => a.Correta);
            return alternativaCorreta?.Resposta ?? "Nenhuma resposta correta definida";
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
            UtilizadaEmTeste = registroEditado.UtilizadaEmTeste;
            
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
