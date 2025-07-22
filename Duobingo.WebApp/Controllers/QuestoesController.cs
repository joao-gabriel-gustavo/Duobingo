using Duobingo.Dominio.ModuloMateria;
using Duobingo.Dominio.ModuloQuestoes;
using Duobingo.Infraestrutura.Orm.Compartilhado;
using Duobingo.WebApp.Extensions;
using Duobingo.WebApp.Model;
using Microsoft.AspNetCore.Mvc;

namespace Duobingo.WebApp.Controllers;

[Route("questoes")]
public class QuestoesController : Controller
{
    private readonly IRepositorioQuestoes repositorioQuestoes;
    private readonly IRepositorioMateria repositorioMateria;
    private readonly duobingoDbContext contexto;

    public QuestoesController(IRepositorioQuestoes repositorioQuestoes, IRepositorioMateria repositorioMateria, duobingoDbContext contexto)
    {
        this.repositorioQuestoes = repositorioQuestoes;
        this.repositorioMateria = repositorioMateria;
        this.contexto = contexto;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var registros = repositorioQuestoes.SelecionarRegistros();
        var visualizarVM = new VisualizarQuestoesViewModel(registros);
        return View(visualizarVM);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        var materiasDisponiveis = repositorioMateria.SelecionarRegistros();
        var cadastrarVM = new CadastrarQuestaoViewModel(materiasDisponiveis);
        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar(CadastrarQuestaoViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            var materiasDisponiveis = repositorioMateria.SelecionarRegistros();
            viewModel.MateriasDisponiveis = materiasDisponiveis.Select(m => new DetalhesMateriaViewModel(m.Id, m.Nome)).ToList();
            return View(viewModel);
        }

        try
        {
            var materia = repositorioMateria.SelecionarRegistroPorId(viewModel.MateriaId);
            if (materia == null)
            {
                ModelState.AddModelError("MateriaId", "Matéria não encontrada.");
                var materiasDisponiveis = repositorioMateria.SelecionarRegistros();
                viewModel.MateriasDisponiveis = materiasDisponiveis.Select(m => new DetalhesMateriaViewModel(m.Id, m.Nome)).ToList();
                return View(viewModel);
            }

            var questao = new Questoes(materia, viewModel.Enunciado);

            var alternativasComTexto = viewModel.Alternativas.Where(a => !string.IsNullOrWhiteSpace(a.Texto)).ToList();
            
            if (alternativasComTexto.Count < 2)
            {
                ModelState.AddModelError("Alternativas", "Uma questão deve ter pelo menos 2 alternativas.");
                var materiasDisponiveis = repositorioMateria.SelecionarRegistros();
                viewModel.MateriasDisponiveis = materiasDisponiveis.Select(m => new DetalhesMateriaViewModel(m.Id, m.Nome)).ToList();
                return View(viewModel);
            }

            if (!alternativasComTexto.Any(a => a.EhCorreta))
            {
                ModelState.AddModelError("Alternativas", "Uma questão deve ter pelo menos uma alternativa correta.");
                var materiasDisponiveis = repositorioMateria.SelecionarRegistros();
                viewModel.MateriasDisponiveis = materiasDisponiveis.Select(m => new DetalhesMateriaViewModel(m.Id, m.Nome)).ToList();
                return View(viewModel);
            }

            foreach (var altVM in alternativasComTexto)
            {
                questao.AdicionarAlternativa(altVM.Texto, altVM.EhCorreta);
            }

            repositorioQuestoes.CadastrarRegistro(questao);
            contexto.SaveChanges();

            TempData["MensagemSucesso"] = "Questão cadastrada com sucesso!";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Erro ao cadastrar questão: {ex.Message}");
            var materiasDisponiveis = repositorioMateria.SelecionarRegistros();
            viewModel.MateriasDisponiveis = materiasDisponiveis.Select(m => new DetalhesMateriaViewModel(m.Id, m.Nome)).ToList();
            return View(viewModel);
        }
    }

    [HttpGet("editar/{id:guid}")]
    public IActionResult Editar(Guid id)
    {
        var questao = repositorioQuestoes.SelecionarRegistroPorId(id);
        if (questao == null)
            return NotFound();

        var materiasDisponiveis = repositorioMateria.SelecionarRegistros();
        var editarVM = new EditarQuestaoViewModel(questao, materiasDisponiveis);
        return View(editarVM);
    }

    [HttpPost("editar/{id:guid}")]
    public IActionResult Editar(Guid id, EditarQuestaoViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            var materiasDisponiveis = repositorioMateria.SelecionarRegistros();
            viewModel.MateriasDisponiveis = materiasDisponiveis.Select(m => new DetalhesMateriaViewModel(m.Id, m.Nome)).ToList();
            return View(viewModel);
        }

        try
        {
            var questaoExistente = repositorioQuestoes.SelecionarRegistroPorId(id);
            if (questaoExistente == null)
                return NotFound();

            var materia = repositorioMateria.SelecionarRegistroPorId(viewModel.MateriaId);
            if (materia == null)
            {
                ModelState.AddModelError("MateriaId", "Matéria não encontrada.");
                var materiasDisponiveis = repositorioMateria.SelecionarRegistros();
                viewModel.MateriasDisponiveis = materiasDisponiveis.Select(m => new DetalhesMateriaViewModel(m.Id, m.Nome)).ToList();
                return View(viewModel);
            }

            var alternativasComTexto = viewModel.Alternativas.Where(a => !string.IsNullOrWhiteSpace(a.Texto)).ToList();
            
            if (alternativasComTexto.Count < 2)
            {
                ModelState.AddModelError("Alternativas", "Uma questão deve ter pelo menos 2 alternativas.");
                var materiasDisponiveis = repositorioMateria.SelecionarRegistros();
                viewModel.MateriasDisponiveis = materiasDisponiveis.Select(m => new DetalhesMateriaViewModel(m.Id, m.Nome)).ToList();
                return View(viewModel);
            }

            if (!alternativasComTexto.Any(a => a.EhCorreta))
            {
                ModelState.AddModelError("Alternativas", "Uma questão deve ter pelo menos uma alternativa correta.");
                var materiasDisponiveis = repositorioMateria.SelecionarRegistros();
                viewModel.MateriasDisponiveis = materiasDisponiveis.Select(m => new DetalhesMateriaViewModel(m.Id, m.Nome)).ToList();
                return View(viewModel);
            }

            questaoExistente.Enunciado = viewModel.Enunciado;
            questaoExistente.Materia = materia;
           // questaoExistente.MateriaId = materia.Id;

            contexto.Alternativas.RemoveRange(questaoExistente.Alternativas);
            questaoExistente.Alternativas.Clear();

            foreach (var altVM in alternativasComTexto)
            {
                var alternativa = new Alternativa(altVM.Texto, altVM.EhCorreta)
                {
                    QuestaoId = questaoExistente.Id,
                    Questao = questaoExistente
                };
                questaoExistente.Alternativas.Add(alternativa);
            }

            contexto.SaveChanges();

            TempData["MensagemSucesso"] = "Questão editada com sucesso!";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Erro ao editar questão: {ex.Message}");
            var materiasDisponiveis = repositorioMateria.SelecionarRegistros();
            viewModel.MateriasDisponiveis = materiasDisponiveis.Select(m => new DetalhesMateriaViewModel(m.Id, m.Nome)).ToList();
            return View(viewModel);
        }
    }

    [HttpGet("excluir/{id:guid}")]
    public IActionResult Excluir(Guid id)
    {
        var questao = repositorioQuestoes.SelecionarRegistroPorId(id);
        if (questao == null)
            return NotFound();

        var excluirVM = new ExcluirQuestaoViewModel(questao);
        return View(excluirVM);
    }

    [HttpPost("excluir/{id:guid}")]
    public IActionResult ConfirmarExclusao(Guid id)
    {
        try
        {
            var questao = repositorioQuestoes.SelecionarRegistroPorId(id);
            if (questao == null)
                return NotFound();

            if (repositorioQuestoes.QuestaoEstaVinculadaATeste(id))
            {
                TempData["MensagemErro"] = "Não é possível excluir esta questão pois ela está vinculada a um teste.";
                return RedirectToAction("Index");
            }

            repositorioQuestoes.ExcluirRegistro(id);
            contexto.SaveChanges();

            TempData["MensagemSucesso"] = "Questão excluída com sucesso!";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            TempData["MensagemErro"] = $"Erro ao excluir questão: {ex.Message}";
            return RedirectToAction("Index");
        }
    }

    [HttpGet("detalhes/{id:guid}")]
    public IActionResult Detalhes(Guid id)
    {
        var questao = repositorioQuestoes.SelecionarRegistroPorId(id);
        if (questao == null)
            return NotFound();

        var detalhesVM = questao.ParaDetalhesVM();
        return View(detalhesVM);
    }
} 