using Duobingo.Dominio.ModuloTeste;
using Duobingo.Infraestrutura.Orm.Compartilhado;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Duobingo.WebApp.Controllers;

[Route("testes")]
public class TesteController : Controller
{
    private readonly IRepositorioTeste repositorioTeste;
    private readonly duobingoDbContext contexto;

    public TesteController(IRepositorioTeste repositorioTeste, duobingoDbContext contexto)
    {
        this.repositorioTeste = repositorioTeste;
        this.contexto = contexto;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var registros = repositorioTeste.SelecionarRegistros();


        return View();
    }

}