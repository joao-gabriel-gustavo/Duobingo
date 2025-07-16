using Duobingo.Dominio.ModuloDespesa;
using Duobingo.Dominio.ModuloCategoria;
using Duobingo.WebApp.Models;

namespace Duobingo.WebApp.Extensions;

public static class DespesaExtensions
{
    public static Despesa ParaEntidade(this FormularioDespesaViewModel formularioVM, List<Categoria> todasCategorias)
    {
        var despesa = new Despesa(formularioVM.Descricao, formularioVM.DataOcorrencia, 
            formularioVM.Valor, formularioVM.FormaPagamento);

        // Adicionar categorias selecionadas
        foreach (var categoriaId in formularioVM.CategoriasSelecionadas)
        {
            var categoria = todasCategorias.FirstOrDefault(c => c.Id == categoriaId);
            if (categoria != null)
            {
                despesa.AdicionarCategoria(categoria);
            }
        }

        return despesa;
    }

    public static DetalhesDespesaViewModel ParaDetalhesVM(this Despesa despesa)
    {
        return new DetalhesDespesaViewModel(
            despesa.Id,
            despesa.Descricao,
            despesa.DataOcorrencia,
            despesa.Valor,
            ObterDescricaoFormaPagamento(despesa.FormaPagamento),
            despesa.Categorias.Select(c => c.Titulo).ToList()
        );
    }

    public static FormularioDespesaViewModel ParaFormularioVM(this Despesa despesa)
    {
        return new FormularioDespesaViewModel
        {
            Descricao = despesa.Descricao,
            DataOcorrencia = despesa.DataOcorrencia,
            Valor = despesa.Valor,
            FormaPagamento = despesa.FormaPagamento,
            CategoriasSelecionadas = despesa.Categorias.Select(c => c.Id).ToList()
        };
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