
using Duobingo.Dominio.Compartilhado;
using Duobingo.Dominio.ModuloMateria;

namespace Duobingo.Dominio.ModuloQuestoes
{
    public class Questoes : EntidadeBase<Questoes>
    {
        public Materia Materia { get; set; }
        public string Enunciado { get; set; }
        public string AlternativaA { get; set; }
        public string AlternativaB { get; set; }
        public string AlternativaC { get; set; }
        public string AlternativaD { get; set; }
        public string RespostaCorreta { get; set; }

        public Questoes(Materia materia, string enunciado, string alternativaA, string alternativaB, string alternativaC, string alternativaD, string respostaCorreta)
        {
            Id = Guid.NewGuid();
            Materia = materia;
            Enunciado = enunciado;
            AlternativaA = alternativaA;
            AlternativaB = alternativaB;
            AlternativaC = alternativaC;
            AlternativaD = alternativaD;
            RespostaCorreta = respostaCorreta;
        }
        
        public override void AtualizarRegistro(Questoes registroEditado)
        {
            Id = registroEditado.Id;
            Materia = registroEditado.Materia;
            Enunciado = registroEditado.Enunciado;
            AlternativaA = registroEditado.AlternativaA;
            AlternativaB = registroEditado.AlternativaB;
            AlternativaC = registroEditado.AlternativaC;
            AlternativaD = registroEditado.AlternativaD;
            RespostaCorreta = registroEditado.RespostaCorreta;
        }
    }
}
