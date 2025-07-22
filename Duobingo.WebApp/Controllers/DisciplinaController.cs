using Duobingo.Dominio.ModuloDisciplina;
using Duobingo.Infraestrutura.Orm.Compartilhado;
using Duobingo.WebApp.Extensions;
using Duobingo.WebApp.Model;
using Microsoft.AspNetCore.Mvc;

namespace Duobingo.WebApp.Controllers;

[Route("disciplinas")]
public class DisciplinaController : Controller
{
    private readonly duobingoDbContext contexto;
    private readonly IRepositorioDisciplina RepositorioDisciplina;

    public DisciplinaController(duobingoDbContext contexto, IRepositorioDisciplina repositorioDisciplina)
    {
        contexto = contexto;
        RepositorioDisciplina = repositorioDisciplina;
    }

    [HttpGet()]
    public IActionResult Index()
    {
        ViewBag.Title = "Gerador de Testes | Disciplinas";

        var disciplinas = RepositorioDisciplina.SelecionarRegistros();
        var visualizarVM = new VisualizarDisciplinaViewModel(disciplinas);

        return View(visualizarVM);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        ViewBag.Title = "Disciplinas | Cadastrar";

        var cadastrarVM = new CadastrarDisciplinaViewModel();

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    [ValidateAntiForgeryToken]
    public IActionResult Cadastrar(CadastrarDisciplinaViewModel cadastrarVm)
    {
        ViewBag.Title = "Disciplinas | Cadastrar";

        var disciplinas = RepositorioDisciplina.SelecionarRegistros();

        foreach (var item in disciplinas)
        {
            if (string.Equals(item.Nome, cadastrarVm.Nome))
            {
                ModelState.AddModelError("CadastroUnico", "Já existe uma disciplina registrada com este nome.");
                return View(cadastrarVm);
            }
        }

        var novaDisciplina = cadastrarVm.ParaEntidade();

        var transacao = contexto.Database.BeginTransaction();

        try
        {
            RepositorioDisciplina.CadastrarRegistro(novaDisciplina);
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
        ViewBag.Title = "Disciplinas | Editar";

        var disciplinaSelecionada = RepositorioDisciplina.SelecionarRegistroPorId(id);

        if (disciplinaSelecionada is null)
            return RedirectToAction(nameof(Index));

        var editarVM = new EditarDisciplinaViewModel(id, disciplinaSelecionada.Nome);

        return View(editarVM);
    }

    [HttpPost("editar/{id:guid}")]
    [ValidateAntiForgeryToken]
    public ActionResult Editar(Guid id, EditarDisciplinaViewModel editarVM)
    {
        ViewBag.Title = "Disciplinas | Editar";

        var disciplinas = RepositorioDisciplina.SelecionarRegistros();

        if (disciplinas.Any(x => !x.Id.Equals(id) && x.Nome.Equals(editarVM.Nome)))
        {
            ModelState.AddModelError("CadastroUnico", "Já existe uma disciplina registrada com este nome.");
            return View(editarVM);
        }

        var disciplinaEditada = editarVM.ParaEntidade();

        var transacao = contexto.Database.BeginTransaction();

        try
        {
            RepositorioDisciplina.EditarRegistro(id, disciplinaEditada);
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
        ViewBag.Title = "Disciplinas | Excluir";

        var disciplinaSelecionada = RepositorioDisciplina.SelecionarRegistroPorId(id);

        if (disciplinaSelecionada is null)
            return RedirectToAction(nameof(Index));

        var excluirVM = new ExcluirDisciplinaViewModel(disciplinaSelecionada.Id, disciplinaSelecionada.Nome);

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:guid}")]
    [ValidateAntiForgeryToken]
    public IActionResult ExcluirConfirmado(Guid id)
    {
        ViewBag.Title = "Disciplinas | Excluir";

        var trasacao = contexto.Database.BeginTransaction();

        try
        {
            RepositorioDisciplina.ExcluirRegistro(id);
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
        ViewBag.Title = "Disciplinas | Detalhes";

        var disciplinaSelecionada = RepositorioDisciplina.SelecionarRegistroPorId(id);

        if (disciplinaSelecionada is null)
            return RedirectToAction(nameof(Index));

        var detalhesVM = new DetalhesDisciplinaViewModel(id, disciplinaSelecionada.Nome);

        return View(detalhesVM);
    }
}