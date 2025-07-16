
using Microsoft.AspNetCore.Mvc;
using Duobingo.Infraestrura.Arquivos.Compartilhado;
using Duobingo.Dominio.ModuloCompromisso;
using Duobingo.Infraestrutura.Aquivos.ModuloCompromisso;
using Duobingo.Dominio.ModuloContato;
using Duobingo.Infraestrura.Arquivos.ModuloContato;
using Duobingo.ConsoleApp.Models;
using Duobingo.WebApp.Extensions;
using Duobingo.WebApp.Models;

namespace Duobingo.WebApp.Controllers
{
    [Route("compromissos")]
    public class CompromissoController : Controller
    {
        private readonly ContextoDados contextoDados;
        private readonly IRepositorioCompromisso repositorioCompromisso;
        private readonly IRepositorioContato repositorioContato;

            public CompromissoController()
            {
                contextoDados = new ContextoDados(true);
                repositorioCompromisso = new RepositorioCompromissoEmArquivo(contextoDados);
                repositorioContato = new RepositorioContatoEmArquivo(contextoDados);
            }   


            [HttpGet()]
            public IActionResult Index()
            {
                var compromissos = repositorioCompromisso.SelecionarRegistros();
                var visualizarVM = new VisualizarCompromissosViewModel(compromissos);
                return View(visualizarVM);
             }

            [HttpGet("cadastrar")]
            public IActionResult Cadastrar()
            {
                var contextoDados = new ContextoDados(true);

                var contatos = repositorioContato.SelecionarRegistros();
                var compromissos = repositorioCompromisso.SelecionarRegistros();

                CadastrarCompromissoViewModel cadastrarVM = new CadastrarCompromissoViewModel(contatos);

                return View(cadastrarVM);
            }

            [HttpPost("cadastrar")]
            public IActionResult Cadastrar(CadastrarCompromissoViewModel cadastrarVM)
            {

                var compromissos = repositorioCompromisso.SelecionarRegistros();
          
                foreach (var item in compromissos)
                {
                    if (item.DataOcorrencia == cadastrarVM.DataOcorrencia && item.HoraInicio == cadastrarVM.HoraInicio)
                    {
                        ModelState.AddModelError("Cadastro Unico", "Já existe um compromisso nesse mesmo dia e horario");
                        break;
                    }
                }
                TimeSpan horaInicio = TimeSpan.ParseExact(cadastrarVM.HoraInicio, @"hh\:mm", null);
                TimeSpan horaTermino = TimeSpan.ParseExact(cadastrarVM.HoraTermino, @"hh\:mm", null);

                if (horaTermino < horaInicio)
                {
                    ModelState.AddModelError("Cadastro Unico", "A hora de término não pode ser anterior à hora de início.");
                }

                if (cadastrarVM.DataOcorrencia < DateTime.Now.Date)
                {
                    ModelState.AddModelError("Cadastro Unico", "A data de ocorrencia nao pode ser menor que a data atual");
                }

            if (cadastrarVM.TipoCompromisso == "Online" && string.IsNullOrWhiteSpace(cadastrarVM.Link))
            {
                ModelState.AddModelError("Link", "Compromissos online devem ter um link");
            }

            if (cadastrarVM.TipoCompromisso == "Presencial" && string.IsNullOrWhiteSpace(cadastrarVM.Local))
            {
                ModelState.AddModelError("Local", "Compromissos presenciais devem ter um local");
            }

            if (!ModelState.IsValid)
                {
                    cadastrarVM.ContatosDisponiveis = repositorioContato.SelecionarRegistros().ParaSelecionarContatoViewModel();
                    return View(cadastrarVM);
                }

            
            var entidade = cadastrarVM.ParaEntidade(repositorioContato.SelecionarRegistros());

            repositorioCompromisso.CadastrarRegistro(entidade);

            return RedirectToAction(nameof(Index));

        }
            
        [HttpGet("editar/{Id:guid}")]
        public IActionResult Editar([FromRoute] Guid id)
        {

            var compromisso = repositorioCompromisso.SelecionarRegistroPorId(id);
            EditarCompromissoViewModel editarVM = compromisso.ParaEditarVM();

            return View(editarVM);
        }

        [HttpPost("editar/{Id:guid}")]
        public IActionResult Editar([FromRoute] Guid id, EditarCompromissoViewModel editarVM)
        {
            var compromissos = repositorioCompromisso.SelecionarRegistros();

            foreach (var item in compromissos)
            {
                if (item.Id == id && item.DataOcorrencia == editarVM.DataOcorrencia && item.HoraInicio == editarVM.HoraInicio)
                {
                    ModelState.AddModelError("Cadastro Unico", "Já existe um compromisso nesse mesmo dia e horario");
                    break;
                }
            }
            TimeSpan horaInicio = TimeSpan.ParseExact(editarVM.HoraInicio, @"hh\:mm", null);
            TimeSpan horaTermino = TimeSpan.ParseExact(editarVM.HoraTermino, @"hh\:mm", null);

            if (horaTermino < horaInicio)
            {
                ModelState.AddModelError("Cadastro Unico", "A hora de término não pode ser anterior à hora de início.");
            }


            if (editarVM.DataOcorrencia < DateTime.Now.Date)
            {
                ModelState.AddModelError("Cadastro Unico", "A data de ocorrencia nao pode ser menor que a data atual");
            }

            if (editarVM.TipoCompromisso == "Presencial" && !string.IsNullOrEmpty(editarVM.Link))
            {
                ModelState.AddModelError("Cadastro Unico", "Compromissos presenciais não devem ter link");
            }

            if (editarVM.TipoCompromisso == "Online" && !string.IsNullOrEmpty(editarVM.Local))
            {
                ModelState.AddModelError("Cadastro Unico", "Compromissos online não devem ter local");
            }

            if (!ModelState.IsValid)
            {
                editarVM.ContatosDisponiveis = repositorioContato.SelecionarRegistros().ParaSelecionarContatoViewModel();
                return View(editarVM);
            }
            var contextoDados = new ContextoDados(true);

            var contatos = repositorioContato.SelecionarRegistros();
            Compromisso compromisso = editarVM.ParaEntidade(repositorioContato.SelecionarRegistros());

            repositorioCompromisso.EditarRegistro(id, compromisso);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet("excluir/{id:guid}")]
        public ActionResult Excluir(Guid id)
        {
            var registroSelecionado = repositorioCompromisso.SelecionarRegistroPorId(id);

            var excluirVM = new ExcluirCompromissoViewModel(registroSelecionado.Id, registroSelecionado.Titulo);

            return View(excluirVM);
        }

        [HttpPost("excluir/{id:guid}")]
        public ActionResult ExcluirConfirmado (Guid id)
        {
            repositorioCompromisso.ExcluirRegistro(id);

            return RedirectToAction(nameof(Index));
        }
        [HttpGet("detalhes/{id:guid}")]
        public ActionResult Detalhes(Guid id)
        {
            var registroSelecionado = repositorioCompromisso.SelecionarRegistroPorId(id);

            DetalhesCompromissoViewModel detalhesVM;
            if (registroSelecionado.Contato != null)
            {
                var detalheVM = new DetalhesCompromissoViewModel(
                    id,
                    registroSelecionado.Titulo,
                    registroSelecionado.Assunto,
                    registroSelecionado.DataOcorrencia,
                    registroSelecionado.HoraInicio,
                    registroSelecionado.HoraTermino,
                    registroSelecionado.TipoCompromisso,
                    registroSelecionado.Local,
                    registroSelecionado.Link,
                    registroSelecionado.Contato.Id
                );
                detalhesVM = detalheVM;
                detalhesVM.NomeContato = registroSelecionado.Contato.Nome;

            }

            else
            {
                var detalheVM = new DetalhesCompromissoViewModel(
                    id,
                    registroSelecionado.Titulo,
                    registroSelecionado.Assunto,
                    registroSelecionado.DataOcorrencia,
                    registroSelecionado.HoraInicio,
                    registroSelecionado.HoraTermino,
                    registroSelecionado.TipoCompromisso,
                    registroSelecionado.Local,
                    registroSelecionado.Link
                );
                detalhesVM = detalheVM;
            }

            return View(detalhesVM);
        }
    }
}