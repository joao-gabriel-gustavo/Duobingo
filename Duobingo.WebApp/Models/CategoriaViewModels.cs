using Duobingo.Dominio.ModuloCategoria;
using Duobingo.WebApp.Extensions;
using System.ComponentModel.DataAnnotations;

namespace Duobingo.WebApp.Models;

public class FormularioCategoriaViewModel
{
    [Required(ErrorMessage = "O campo \"Título\" é obrigatório.")]
    [MinLength(2, ErrorMessage = "O campo \"Título\" precisa conter ao menos 2 caracteres.")]
    [MaxLength(100, ErrorMessage = "O campo \"Título\" precisa conter no máximo 100 caracteres.")]
    public string Titulo { get; set; }
}

public class CadastrarCategoriaViewModel : FormularioCategoriaViewModel
{
    public CadastrarCategoriaViewModel() { }

    public CadastrarCategoriaViewModel(string titulo) : this()
    {
        Titulo = titulo;
    }
}

public class EditarCategoriaViewModel : FormularioCategoriaViewModel
{
    public Guid Id { get; set; }

    public EditarCategoriaViewModel() { }

    public EditarCategoriaViewModel(Guid id, string titulo) : this()
    {
        Id = id;
        Titulo = titulo;
    }
}

public class ExcluirCategoriaViewModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public int QuantidadeDespesas { get; set; }

    public ExcluirCategoriaViewModel(Guid id, string titulo, int quantidadeDespesas)
    {
        Id = id;
        Titulo = titulo;
        QuantidadeDespesas = quantidadeDespesas;
    }
}

public class VisualizarCategoriasViewModel
{
    public List<DetalhesCategoriaViewModel> Registros { get; set; }

    public VisualizarCategoriasViewModel(List<Categoria> categorias)
    {
        Registros = new List<DetalhesCategoriaViewModel>();

        foreach (var c in categorias)
            Registros.Add(c.ParaDetalhesVM());
    }
}

public class DetalhesCategoriaViewModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public int QuantidadeDespesas { get; set; }
    public decimal ValorTotalDespesas { get; set; }
    public List<DetalhesDespesaViewModel> Despesas { get; set; }

    public DetalhesCategoriaViewModel(Guid id, string titulo, int quantidadeDespesas, decimal valorTotalDespesas, List<DetalhesDespesaViewModel> despesas)
    {
        Id = id;
        Titulo = titulo;
        QuantidadeDespesas = quantidadeDespesas;
        ValorTotalDespesas = valorTotalDespesas;
        Despesas = despesas ?? new List<DetalhesDespesaViewModel>();
    }
}

public class SelecionarCategoriaViewModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }

    public SelecionarCategoriaViewModel(Guid id, string titulo)
    {
        Id = id;
        Titulo = titulo;
    }
} 