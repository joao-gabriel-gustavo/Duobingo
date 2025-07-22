using Duobingo.Dominio.ModuloDisciplina;
using Duobingo.Dominio.ModuloMateria;
using Duobingo.Infraestrutura.Orm.Compartilhado;
using Duobingo.WebApp.Extensions;
using Duobingo.WebApp.Model;
using Microsoft.AspNetCore.Mvc;

namespace Duobingo.WebApp.Controllers
{
    [Route("materias")]
    public class MateriaController : Controller
    {
        private readonly duobingoDbContext contexto;
        private readonly IRepositorioMateria repositorioMateria;
        private readonly IRepositorioDisciplina repositorioDisciplina;

        public MateriaController(duobingoDbContext contexto, IRepositorioMateria repositorioMateria, IRepositorioDisciplina repositorioDisciplina)
        {
            this.contexto = contexto;
            this.repositorioMateria = repositorioMateria;
            this.repositorioDisciplina = repositorioDisciplina;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Gerador de Testes | Materias";

            var visualizarVm = new VisualizarMateriaViewModel(repositorioMateria.SelecionarRegistros());

            return View(visualizarVm);
        }

        [HttpGet("cadastrar")]
        public IActionResult Cadastrar()
        {
            ViewBag.Title = "Materias | Cadastrar";

            var disciplinas = repositorioDisciplina.SelecionarRegistros();

            var cadastrarVM = new CadastrarMateriaViewModel(disciplinas);

            return View(cadastrarVM);
        }


        [HttpPost("cadastrar")]
        [ValidateAntiForgeryToken]
        public IActionResult Cadastrar(CadastrarMateriaViewModel cadastrarVm)
        {
            ViewBag.Title = "Materias | Cadastrar";

            var materias = repositorioMateria.SelecionarRegistros();
            var disciplinas = repositorioDisciplina.SelecionarRegistros();

            foreach (var item in materias)
            {
                if (item.Nome.Equals(cadastrarVm.Nome))
                {
                    ModelState.AddModelError("CadastroUnico", "Já existe uma materia registrada com este nome.");
                    return View(cadastrarVm);
                }
            }

            var novaDisciplina = cadastrarVm.ParaEntidade(disciplinas);

            var transacao = contexto.Database.BeginTransaction();

            try
            {
                repositorioMateria.CadastrarRegistro(novaDisciplina);
                contexto.SaveChanges();
                transacao.Commit();
            }
            catch (Exception)
            {
                transacao.Rollback();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("editar/{id:guid}")]
        public IActionResult Editar(Guid id)
        {
            ViewBag.Title = "Materias | Editar";

            var materiaSelecionada = repositorioMateria.SelecionarRegistroPorId(id);
            var disciplinas = repositorioDisciplina.SelecionarRegistros();

            if (materiaSelecionada is null)
                return RedirectToAction(nameof(Index));

            var editarVM = new EditarMateriaViewModel(id, materiaSelecionada.Nome, materiaSelecionada.Serie, disciplinas, materiaSelecionada.Disciplina.Id);

            return View(editarVM);
        }

        [HttpPost("editar/{id:guid}")]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Guid id, EditarMateriaViewModel editarVM)
        {
            var materias = repositorioMateria.SelecionarRegistros();
            var disciplinas = repositorioDisciplina.SelecionarRegistros();

            if (materias.Any(x => !x.Id.Equals(id) && x.Nome.Equals(editarVM.Nome)))
            {
                ModelState.AddModelError("CadastroUnico", "Já existe uma disciplina registrada com este nome.");
                return View(editarVM);
            }

            var materiaEditada = editarVM.ParaEntidade(disciplinas);

            var transacao = contexto.Database.BeginTransaction();

            try
            {
                repositorioMateria.EditarRegistro(id, materiaEditada);
                contexto.SaveChanges();
                transacao.Commit();
            }
            catch (Exception)
            {
                transacao.Rollback();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("excluir/{id:guid}")]
        public IActionResult Excluir(Guid id)
        {
            ViewBag.Title = "Materias | Excluir";

            var materiaSelecionada = repositorioMateria.SelecionarRegistroPorId(id);

            if (materiaSelecionada is null)
                return RedirectToAction(nameof(Index));

            var excluirVM = new ExcluirMateriaViewModel(materiaSelecionada.Id, materiaSelecionada.Nome);

            return View(excluirVM);
        }

        [HttpPost("excluir/{id:guid}")]
        [ValidateAntiForgeryToken]
        public IActionResult ExcluirConfirmado(Guid id)
        {
            var trasacao = contexto.Database.BeginTransaction();

            try
            {
                repositorioMateria.ExcluirRegistro(id);
                contexto.SaveChanges();
                trasacao.Commit();
            }
            catch (Exception)
            {
                trasacao.Rollback();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("detalhes/{id:guid}")]
        public IActionResult Detalhes(Guid id)
        {
            ViewBag.Title = ("Materias | Detalhes");

            var materiaSelecionada = repositorioMateria.SelecionarRegistroPorId(id);

            if (materiaSelecionada is null)
                return RedirectToAction(nameof(Index));

            var detalhesVM = new DetalhesMateriaViewModel(id, materiaSelecionada.Nome, materiaSelecionada.Serie, materiaSelecionada.Disciplina.Nome);

            return View(detalhesVM);
        }
    }
}
