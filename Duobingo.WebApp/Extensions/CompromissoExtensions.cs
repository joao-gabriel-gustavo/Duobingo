using Duobingo.ConsoleApp.Models;
using Duobingo.Dominio.ModuloCompromisso;
using Duobingo.Dominio.ModuloContato;
using Duobingo.WebApp.Models;

namespace Duobingo.WebApp.Extensions
{
    public static class CompromissoExtensions
    {
        public static Compromisso ParaEntidade(this FormularioCompromissoViewModel formularioVM, List<Contato> contatos)
        {
            Contato ContatoSelecionado = null;
            foreach (var f in contatos)
            {
                if (f.Id == formularioVM.ContatoId)
                {
                   ContatoSelecionado = f;
                }
            }
            return new Compromisso(formularioVM.Titulo, formularioVM.Assunto, formularioVM.DataOcorrencia, formularioVM.HoraInicio, formularioVM.HoraTermino, formularioVM.TipoCompromisso,formularioVM.Local, formularioVM.Link, ContatoSelecionado);
        }
        public static DetalhesCompromissoViewModel ParaDetalheVM(this Compromisso compromisso)
        {

            if (compromisso.Contato != null)
            {
                    return new DetalhesCompromissoViewModel(
                            compromisso.Id,
                            compromisso.Titulo,
                            compromisso.Assunto,
                            compromisso.DataOcorrencia,
                            compromisso.HoraInicio,
                            compromisso.HoraTermino,
                            compromisso.TipoCompromisso,
                            compromisso.Local,
                            compromisso.Link,
                            compromisso.Contato.Id

                            );
               
            }
            else
            {
                    return new DetalhesCompromissoViewModel(
                            compromisso.Id,
                            compromisso.Titulo,
                            compromisso.Assunto,
                            compromisso.DataOcorrencia,
                            compromisso.HoraInicio,
                            compromisso.HoraTermino,
                            compromisso.TipoCompromisso,
                            compromisso.Local,
                            compromisso.Link
                    );
                
            }
        }

        public static List<SelecionarContatoViewModel> ParaSelecionarContatoViewModel(this List<Contato> contatos)
        {
            List<SelecionarContatoViewModel> contatosVM = new List<SelecionarContatoViewModel>();
            foreach (Contato contato in contatos)
            {
                var contatoVM = new SelecionarContatoViewModel(contato.Id, contato.Nome);
                contatosVM.Add(contatoVM);
            }
            return contatosVM;
        }


        public static EditarCompromissoViewModel ParaEditarVM(this Compromisso compromisso)
        {
            if (compromisso.Contato != null)
            {
                return new EditarCompromissoViewModel(
                        compromisso.Id,
                        compromisso.Titulo,
                        compromisso.Assunto,
                        compromisso.DataOcorrencia,
                        compromisso.HoraInicio,
                        compromisso.HoraTermino,
                        compromisso.TipoCompromisso,
                        compromisso.Local,
                        compromisso.Link,
                        compromisso.Contato.Id
                );
            }
            else
            {
                return new EditarCompromissoViewModel(
                        compromisso.Id,
                        compromisso.Titulo,
                        compromisso.Assunto,
                        compromisso.DataOcorrencia,
                        compromisso.HoraInicio,
                        compromisso.HoraTermino,
                        compromisso.TipoCompromisso,
                        compromisso.Local,
                        compromisso.Link
                );
            }
        }
    }
}
