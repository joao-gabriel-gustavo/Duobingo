using Duobingo.Dominio.ModuloContato;
using Duobingo.WebApp.Extensions;
using Duobingo.WebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace Duobingo.WebApp.Models
{
    public abstract class FormularioContatoViewModel
    {

        [Required(ErrorMessage = "O Campo nome é obrigatório")]
        [MinLength(3, ErrorMessage = "O Campo nome deve conter ao minimo 3 caracteres")]
        [MaxLength(100, ErrorMessage = "O Campo nome deve conter ao maximo 100 caracteres")]
        public string Nome { get; set; }



        [Required(ErrorMessage = "O Campo email é obrigatório")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "O campo email deve serguir um padrão correto")]
        public string Email { get; set; }


        [Required(ErrorMessage = "O Campo telefone é obrigatório")]
        public string Telefone { get; set; }


        public string Cargo { get; set; }


        public string Empresa { get; set; }
    }

    public class CadastrarContatoViewModel : FormularioContatoViewModel
    {
        public CadastrarContatoViewModel() { }

        public CadastrarContatoViewModel(string nome, string email, string telefone, string cargo, string empresa) : this()
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Cargo = cargo;
            Empresa = empresa;
        }
    }


    public class EditarContatoViewModel : FormularioContatoViewModel
    {

        public Guid Id { get; set; }
        public EditarContatoViewModel() { }

        public EditarContatoViewModel(Guid id, string nome, string email, string telefone, string cargo, string empresa) : this()
        {
            Id = id;
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Cargo = cargo;
            Empresa = empresa;
        }
    }


    public class ExcluirContatoViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public ExcluirContatoViewModel() { }
        public ExcluirContatoViewModel(Guid id, string nome) : this()
        {
            Id = id;
            Nome = nome;
        }
    }


    public class VisualizarContatosViewModel
    {
        public List<DetalhesContatoViewModel> Registros { get; }
        public VisualizarContatosViewModel(List<Contato> produtos)
        {
            Registros = [];

            foreach (var produto in produtos)
            {
                var detalhesVM = produto.ParaDetalhesVM();

                Registros.Add(detalhesVM);
            }
        }
    }


    public class  DetalhesContatoViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Cargo { get; set; }
        public string Empresa { get; set; }
        public DetalhesContatoViewModel(Guid id, string nome, string email, string telefone, string cargo, string empresa)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Cargo = cargo;
            Empresa = empresa;
        }
    }

}
