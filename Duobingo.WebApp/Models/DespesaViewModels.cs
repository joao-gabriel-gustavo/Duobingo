using Duobingo.Dominio.ModuloDespesa;
using Duobingo.Dominio.ModuloCategoria;
using Duobingo.WebApp.Extensions;
using System.ComponentModel.DataAnnotations;

namespace Duobingo.WebApp.Models;

public class FormularioDespesaViewModel
{
    [Required(ErrorMessage = "O campo \"Descrição\" é obrigatório.")]
    [MinLength(2, ErrorMessage = "O campo \"Descrição\" precisa conter ao menos 2 caracteres.")]
    [MaxLength(100, ErrorMessage = "O campo \"Descrição\" precisa conter no máximo 100 caracteres.")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "O campo \"Data de Ocorrência\" é obrigatório.")]
    [DataType(DataType.Date)]
    public DateTime DataOcorrencia { get; set; }

    [Required(ErrorMessage = "O campo \"Valor\" é obrigatório.")]
    [DataType(DataType.Currency)]
    [Range(0.01, double.MaxValue, ErrorMessage = "O campo \"Valor\" precisa ser maior que zero.")]
    public decimal Valor { get; set; }

    [Required(ErrorMessage = "O campo \"Forma de Pagamento\" é obrigatório.")]
    public FormaPagamentoEnum FormaPagamento { get; set; }

    [Required(ErrorMessage = "Selecione pelo menos uma categoria.")]
    public List<Guid> CategoriasSelecionadas { get; set; } = new List<Guid>();

    public List<SelecionarCategoriaViewModel> CategoriasDisponiveis { get; set; } = new List<SelecionarCategoriaViewModel>();

    public FormularioDespesaViewModel()
    {
        DataOcorrencia = DateTime.Now.Date;
    }
}

public class CadastrarDespesaViewModel : FormularioDespesaViewModel
{
    public CadastrarDespesaViewModel() { }

    public CadastrarDespesaViewModel(string descricao, DateTime dataOcorrencia, decimal valor, 
        FormaPagamentoEnum formaPagamento) : this()
    {
        Descricao = descricao;
        DataOcorrencia = dataOcorrencia;
        Valor = valor;
        FormaPagamento = formaPagamento;
    }
}

public class EditarDespesaViewModel : FormularioDespesaViewModel
{
    public Guid Id { get; set; }

    public EditarDespesaViewModel() { }

    public EditarDespesaViewModel(Guid id, string descricao, DateTime dataOcorrencia, 
        decimal valor, FormaPagamentoEnum formaPagamento) : this()
    {
        Id = id;
        Descricao = descricao;
        DataOcorrencia = dataOcorrencia;
        Valor = valor;
        FormaPagamento = formaPagamento;
    }
}

public class ExcluirDespesaViewModel
{
    public Guid Id { get; set; }
    public string Descricao { get; set; }
    public DateTime DataOcorrencia { get; set; }
    public decimal Valor { get; set; }
    public string FormaPagamento { get; set; }
    public List<string> Categorias { get; set; }

    public ExcluirDespesaViewModel(Guid id, string descricao, DateTime dataOcorrencia, 
        decimal valor, string formaPagamento, List<string> categorias)
    {
        Id = id;
        Descricao = descricao;
        DataOcorrencia = dataOcorrencia;
        Valor = valor;
        FormaPagamento = formaPagamento;
        Categorias = categorias;
    }
}

public class VisualizarDespesasViewModel
{
    public List<DetalhesDespesaViewModel> Registros { get; set; }
    public decimal ValorTotal { get; set; }

    public VisualizarDespesasViewModel(List<Despesa> despesas)
    {
        Registros = new List<DetalhesDespesaViewModel>();

        foreach (var d in despesas)
            Registros.Add(d.ParaDetalhesVM());

        ValorTotal = despesas.Sum(d => d.Valor);
    }
}

public class DetalhesDespesaViewModel
{
    public Guid Id { get; set; }
    public string Descricao { get; set; }
    public DateTime DataOcorrencia { get; set; }
    public decimal Valor { get; set; }
    public string FormaPagamento { get; set; }
    public List<string> Categorias { get; set; }

    public DetalhesDespesaViewModel(Guid id, string descricao, DateTime dataOcorrencia, 
        decimal valor, string formaPagamento, List<string> categorias)
    {
        Id = id;
        Descricao = descricao;
        DataOcorrencia = dataOcorrencia;
        Valor = valor;
        FormaPagamento = formaPagamento;
        Categorias = categorias;
    }
} 