﻿
using Duobingo.Dominio.Compartilhado;
using Duobingo.Dominio.ModuloDisciplina;

namespace Duobingo.Dominio.ModuloMateria
{
    public class Materia : EntidadeBase<Materia>
    {
        public string Nome { get; set; }
        public Disciplina Disciplina { get; set; }
        public string Serie { get; set; }

        public Materia(string nome, Disciplina disciplina, string serie)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Disciplina = disciplina;
            Serie = serie;
        }

        public override void AtualizarRegistro(Materia registroEditado)
        {
            Nome = registroEditado.Nome;
            Disciplina = registroEditado.Disciplina;
            Serie = registroEditado.Serie;
        }
    }
}
