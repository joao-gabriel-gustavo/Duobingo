using Duobingo.Dominio.ModuloDespesa;
using Duobingo.Dominio.ModuloCategoria;
using Duobingo.Infraestrura.Arquivos.Compartilhado;
using Duobingo.Infraestrura.Arquivos.ModuloDespesa;
using Duobingo.Infraestrura.Arquivos.ModuloCategoria;
using Duobingo.WebApp.Extensions;
using Duobingo.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Duobingo.WebApp.Controllers;

[Route("despesas")]
public class DespesaController : Controller
{
    private readonly ContextoDados contextoDados;
    private readonly IRepositorioDespesa repositorioDespesa;
    private readonly IRepositorioCategoria repositorioCategoria;
    private readonly ValidadorDespesa validadorDespesa;

    public DespesaController()
    {
        contextoDados = new ContextoDados(true);
        repositorioDespesa = new RepositorioDespesaEmArquivo(contextoDados);
        repositorioCategoria = new RepositorioCategoriaEmArquivo(contextoDados);
        validadorDespesa = new ValidadorDespesa();
    }

    [HttpGet]
    public IActionResult Index()
    {
        var registros = repositorioDespesa.SelecionarRegistros();

        var visualizarVM = new VisualizarDespesasViewModel(registros);

        return View(visualizarVM);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        var cadastrarVM = new CadastrarDespesaViewModel();
        
        CarregarCategoriasDisponiveis(cadastrarVM);

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    [ValidateAntiForgeryToken]
    public IActionResult Cadastrar(CadastrarDespesaViewModel cadastrarVM)
    {
        var todasCategorias = repositorioCategoria.SelecionarRegistros();
        var entidade = cadastrarVM.ParaEntidade(todasCategorias);

        var errosValidacao = validadorDespesa.Validar(entidade);

        foreach (var erro in errosValidacao)
            ModelState.AddModelError("", erro);

        if (!ModelState.IsValid)
        {
            CarregarCategoriasDisponiveis(cadastrarVM);
            return View(cadastrarVM);
        }

        repositorioDespesa.CadastrarRegistro(entidade);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("editar/{id:guid}")]
    public IActionResult Editar(Guid id)
    {
        var registroSelecionado = repositorioDespesa.SelecionarRegistroPorId(id);

        if (registroSelecionado == null)
            return NotFound();

        var editarVM = new EditarDespesaViewModel(
            id,
            registroSelecionado.Descricao,
            registroSelecionado.DataOcorrencia,
            registroSelecionado.Valor,
            registroSelecionado.FormaPagamento
        );

        editarVM.CategoriasSelecionadas = registroSelecionado.Categorias.Select(c => c.Id).ToList();
        
        CarregarCategoriasDisponiveis(editarVM);

        return View(editarVM);
    }

    [HttpPost("editar/{id:guid}")]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(Guid id, EditarDespesaViewModel editarVM)
    {
        var todasCategorias = repositorioCategoria.SelecionarRegistros();
        var entidadeEditada = editarVM.ParaEntidade(todasCategorias);

        var errosValidacao = validadorDespesa.Validar(entidadeEditada);

        foreach (var erro in errosValidacao)
            ModelState.AddModelError("", erro);

        if (!ModelState.IsValid)
        {
            CarregarCategoriasDisponiveis(editarVM);
            return View(editarVM);
        }

        repositorioDespesa.EditarRegistro(id, entidadeEditada);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("excluir/{id:guid}")]
    public IActionResult Excluir(Guid id)
    {
        var registroSelecionado = repositorioDespesa.SelecionarRegistroPorId(id);

        if (registroSelecionado == null)
            return NotFound();

        var excluirVM = new ExcluirDespesaViewModel(
            registroSelecionado.Id,
            registroSelecionado.Descricao,
            registroSelecionado.DataOcorrencia,
            registroSelecionado.Valor,
            ObterDescricaoFormaPagamento(registroSelecionado.FormaPagamento),
            registroSelecionado.Categorias.Select(c => c.Titulo).ToList()
        );

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:guid}")]
    public IActionResult ExcluirConfirmado(Guid id)
    {
        repositorioDespesa.ExcluirRegistro(id);

        return RedirectToAction(nameof(Index));
    }

    private void CarregarCategoriasDisponiveis(FormularioDespesaViewModel formularioVM)
    {
        var categorias = repositorioCategoria.SelecionarRegistros();
        
        formularioVM.CategoriasDisponiveis = categorias
            .Select(c => c.ParaSelecionarVM())
            .ToList();
    }

    private static string ObterDescricaoFormaPagamento(FormaPagamentoEnum formaPagamento)
    {
        return formaPagamento switch
        {
            FormaPagamentoEnum.AVista => "À Vista",
            FormaPagamentoEnum.Credito => "Crédito",
            FormaPagamentoEnum.Debito => "Débito",
            _ => formaPagamento.ToString()
        };
    }
} 