using Duobingo.Dominio.ModuloDisciplina;
using Duobingo.Dominio.ModuloMateria;
using Duobingo.Dominio.ModuloTeste;
using Duobingo.Infraestrutura.Orm.Compartilhado;
using Duobingo.WebApp.Extensions;
using Duobingo.WebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Duobingo.WebApp.Controllers;

[Route("testes")]
public class TesteController : Controller
{
    private readonly IRepositorioTeste repositorioTeste;
    private readonly IRepositorioMateria repositorioMateria;
    private readonly IRepositorioDisciplina repositorioDisciplina;
    private readonly duobingoDbContext contexto;

    public TesteController(IRepositorioTeste repositorioTeste, IRepositorioDisciplina repositorioDisciplina, IRepositorioMateria repositorioMateria, duobingoDbContext contexto)
    {
        this.repositorioTeste = repositorioTeste;
        this.repositorioMateria = repositorioMateria;
        this.repositorioDisciplina = repositorioDisciplina;
        this.contexto = contexto;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var registros = repositorioTeste.SelecionarRegistros();

        var visualizarVM = new VisualizarTestesViewModel(registros);
        return View(visualizarVM);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        var materiasDisponiveis = repositorioMateria.SelecionarRegistros();
        var disciplinasDisponiveis = repositorioDisciplina.SelecionarRegistros();
        var cadastrarVM = new CadastrarTesteViewModel(materiasDisponiveis, disciplinasDisponiveis);

        return View(cadastrarVM);
    }


    [HttpPost("cadastrar")]
    [ValidateAntiForgeryToken]
    public IActionResult Cadastrar(CadastrarTesteViewModel cadastrarVM)
    {
        var materiasDisponiveis = repositorioMateria.SelecionarRegistros();
        var disciplinasDisponiveis = repositorioDisciplina.SelecionarRegistros();

        if (!ModelState.IsValid)
        {
            foreach (var m in materiasDisponiveis)
            {
                var selecionarVM = new DetalhesMateriaViewModel(m.Id, m.Nome);

                cadastrarVM.MateriasDisponiveis?.Add(selecionarVM);
            }

            foreach (var d in materiasDisponiveis)
            {
                var selecionarVM = new DetalhesDisciplinaViewModel(d.Id, d.Nome);

                cadastrarVM.DisciplinasDisponiveis?.Add(selecionarVM);
            }
            return View(cadastrarVM);
        }


        var entidade = cadastrarVM.ParaEntidade(disciplinasDisponiveis);
      
       
        foreach( var ms in cadastrarVM.MateriasSelecionadas)
        {
            foreach(var md in cadastrarVM.MateriasDisponiveis)
            {
               if (md.Id.Equals(ms))
                {
                    foreach(var mi in repositorioMateria.SelecionarRegistros())
                    {
                        if (mi.Id == md.Id)
                        {
                            entidade.Materia.Add(new Materia(md.Nome, entidade.Disciplina, mi.Serie));
                            entidade.Serie = mi.Serie;
                        }
                                
                    }
                }
            }
        }
        var transacao = contexto.Database.BeginTransaction();

        try
        {
            repositorioTeste.CadastrarRegistro(entidade);
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


}