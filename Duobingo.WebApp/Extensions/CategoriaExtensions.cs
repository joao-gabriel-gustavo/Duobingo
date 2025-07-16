using Duobingo.Dominio.ModuloCategoria;
using Duobingo.WebApp.Models;

namespace Duobingo.WebApp.Extensions;

public static class CategoriaExtensions
{
    public static Categoria ParaEntidade(this FormularioCategoriaViewModel formularioVM)
    {
        return new Categoria(formularioVM.Titulo);
    }

    public static DetalhesCategoriaViewModel ParaDetalhesVM(this Categoria categoria)
    {
        return new DetalhesCategoriaViewModel(
            categoria.Id,
            categoria.Titulo,
            categoria.Despesas.Count,
            categoria.CalcularValorTotalDespesas(),
            categoria.Despesas.Select(d => d.ParaDetalhesVM()).ToList()
        );
    }

    public static SelecionarCategoriaViewModel ParaSelecionarVM(this Categoria categoria)
    {
        return new SelecionarCategoriaViewModel(categoria.Id, categoria.Titulo);
    }
} 