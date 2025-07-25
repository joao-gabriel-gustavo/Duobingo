﻿using System;
using Duobingo.Dominio.ModuloDisciplina;
using Duobingo.Dominio.ModuloMateria;
using Duobingo.Dominio.ModuloTeste;
using Duobingo.Infraestrutura.Orm.Compartilhado;
using Duobingo.WebApp.Extensions;
using Duobingo.WebApp.Model;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("etapaum")]
    public IActionResult CadastrarPrimeiraEtapa(CadastrarTesteViewModel cadastrarVM)
    {
        var materiasDisponiveis = repositorioMateria.SelecionarRegistros();
        var disciplinasDisponiveis = repositorioDisciplina.SelecionarRegistros();

        Disciplina Disciplina = new Disciplina();

        foreach (var m in repositorioDisciplina.SelecionarRegistros())
        {
            if (cadastrarVM.DisciplinaId == m.Id)
            {
                Disciplina.Id = m.Id;
                Disciplina.Nome = m.Nome;
                Disciplina.Materias = m.Materias;
            }
        }

        foreach (var s in Disciplina.Materias)
        {
            cadastrarVM.SeriesDisponiveis.Add(s.Serie.ToString());
        }

        return View(cadastrarVM);
    }


    [HttpPost("cadastrar")]
    public IActionResult Cadastrar(CadastrarTesteViewModel cadastrarVM)
    {
        var materiasDisponiveis = repositorioMateria.SelecionarRegistros();
        var disciplinasDisponiveis = repositorioDisciplina.SelecionarRegistros();

        if (!ModelState.IsValid)
        {
            foreach (var m in materiasDisponiveis)
            {
                var selecionarVM = new DetalhesMateriaTesteViewModel(m.Id, m.Nome);

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


        if (entidade.EhRecuperacao)
        {
            foreach (var M in entidade.Disciplina.Materias)
            {
                foreach (var q in M.Questoes)
                {
                    entidade.Questoes.Add(q);
                }
            }
            var random = new Random();
            entidade.Questoes = entidade.Questoes.OrderBy(q => random.Next()).Take(entidade.QuantidadeQuestoes).ToList();

        }

        if (!entidade.EhRecuperacao)
        {
           
                foreach (var q in entidade.Materia.Questoes)
                {
                    entidade.Questoes.Add(q);
                }
            

            var random = new Random();
            entidade.Questoes = entidade.Questoes.OrderBy(q => random.Next()).ToList();
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


        return View(cadastrarVM);
    }


}