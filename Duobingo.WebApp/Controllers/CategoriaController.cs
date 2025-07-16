using Duobingo.Dominio.ModuloCategoria;
using Duobingo.Infraestrura.Arquivos.Compartilhado;
using Duobingo.Infraestrura.Arquivos.ModuloCategoria;
using Duobingo.WebApp.Extensions;
using Duobingo.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Duobingo.WebApp.Controllers;

[Route("categorias")]
public class CategoriaController : Controller
{
    private readonly ContextoDados contextoDados;
    private readonly IRepositorioCategoria repositorioCategoria;
    private readonly ValidadorCategoria validadorCategoria;

    public CategoriaController()
    {
        contextoDados = new ContextoDados(true);
        repositorioCategoria = new RepositorioCategoriaEmArquivo(contextoDados);
        validadorCategoria = new ValidadorCategoria();
    }

    [HttpGet]
    public IActionResult Index()
    {
        var registros = repositorioCategoria.SelecionarRegistros();

        var visualizarVM = new VisualizarCategoriasViewModel(registros);

        return View(visualizarVM);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        var cadastrarVM = new CadastrarCategoriaViewModel();

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    [ValidateAntiForgeryToken]
    public IActionResult Cadastrar(CadastrarCategoriaViewModel cadastrarVM)
    {
        var entidade = cadastrarVM.ParaEntidade();

        var errosValidacao = validadorCategoria.Validar(entidade);

        foreach (var erro in errosValidacao)
            ModelState.AddModelError("", erro);

        if (repositorioCategoria.ExisteTituloCategoria(entidade.Titulo))
        {
            ModelState.AddModelError("CadastroUnico", "Já existe uma categoria registrada com este título.");
        }

        if (!ModelState.IsValid)
            return View(cadastrarVM);

        repositorioCategoria.CadastrarRegistro(entidade);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("editar/{id:guid}")]
    public IActionResult Editar(Guid id)
    {
        var registroSelecionado = repositorioCategoria.SelecionarRegistroPorId(id);

        if (registroSelecionado == null)
            return NotFound();

        var editarVM = new EditarCategoriaViewModel(id, registroSelecionado.Titulo);

        return View(editarVM);
    }

    [HttpPost("editar/{id:guid}")]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(Guid id, EditarCategoriaViewModel editarVM)
    {
        var entidadeEditada = editarVM.ParaEntidade();

        var errosValidacao = validadorCategoria.Validar(entidadeEditada);

        foreach (var erro in errosValidacao)
            ModelState.AddModelError("", erro);

        if (repositorioCategoria.ExisteTituloCategoria(entidadeEditada.Titulo, id))
        {
            ModelState.AddModelError("CadastroUnico", "Já existe uma categoria registrada com este título.");
        }

        if (!ModelState.IsValid)
            return View(editarVM);

        repositorioCategoria.EditarRegistro(id, entidadeEditada);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("detalhes/{id:guid}")]
    public IActionResult Detalhes(Guid id)
    {
        var registroSelecionado = repositorioCategoria.SelecionarRegistroPorId(id);

        if (registroSelecionado == null)
            return NotFound();

        var detalhesVM = registroSelecionado.ParaDetalhesVM();

        return View(detalhesVM);
    }

    [HttpGet("excluir/{id:guid}")]
    public IActionResult Excluir(Guid id)
    {
        var registroSelecionado = repositorioCategoria.SelecionarRegistroPorId(id);

        if (registroSelecionado == null)
            return NotFound();

        var excluirVM = new ExcluirCategoriaViewModel(
            registroSelecionado.Id, 
            registroSelecionado.Titulo,
            registroSelecionado.Despesas.Count
        );

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:guid}")]
    public IActionResult ExcluirConfirmado(Guid id)
    {
        var registroSelecionado = repositorioCategoria.SelecionarRegistroPorId(id);

        if (registroSelecionado == null)
            return NotFound();

        var errosValidacao = validadorCategoria.ValidarExclusao(registroSelecionado);

        if (errosValidacao.Count > 0)
        {
            TempData["MensagemErro"] = string.Join(", ", errosValidacao);
            return RedirectToAction(nameof(Index));
        }

        repositorioCategoria.ExcluirRegistro(id);

        return RedirectToAction(nameof(Index));
    }
} 