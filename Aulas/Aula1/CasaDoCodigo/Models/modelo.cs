using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CasaDoCodigo.Models
{
    [DataContract]
    public abstract class BaseModel
    {
        [DataMember]
        public int Id { get; protected set; }
    }

    public class Produto : BaseModel
    {
        public Produto()
        {

        }

        [Required]
        public string Codigo { get; private set; }
        [Required]
        public string Nome { get; private set; }
        [Required]
        public decimal Preco { get; private set; }
        [Required]
        public Categoria Categoria { get; private set; }
        
        public string ScrImagem { get; internal set; }

        public Produto(string codigo, string nome, decimal preco, Categoria categoria, string src)
        {
            this.Codigo = codigo;
            this.Nome = nome;
            this.Preco = preco;
            this.Categoria = categoria;
            this.ScrImagem = src;
        }
    }

    public class Cadastro : BaseModel
    {
        public Cadastro()
        {
        }

        public virtual Pedido Pedido { get; set; }
        [MinLength(5, ErrorMessage = "Nome deve conter pelo menos 5 caracteres")]
        [Required(ErrorMessage = "Nome é Obrigatório")]
        public string Nome { get; set; } = "";
        [Required(ErrorMessage = "Email é Obrigatório")]
        public string Email { get; set; } = "";
        [Required(ErrorMessage = "Telefone é Obrigatório")]
        public string Telefone { get; set; } = "";
        [Required(ErrorMessage = "Endereço é Obrigatório")]
        public string Endereco { get; set; } = "";
        [Required(ErrorMessage = "COmplemento é Obrigatório")]
        public string Complemento { get; set; } = "";
        [Required(ErrorMessage = "Bairro é Obrigatório")]
        public string Bairro { get; set; } = "";
        [Required(ErrorMessage = "Município é Obrigatório")]
        public string Municipio { get; set; } = "";
        [Required(ErrorMessage = "UF é Obrigatório")]
        public string UF { get; set; } = "";
        [Required(ErrorMessage = "CEP é Obrigatório")]
        public string CEP { get; set; } = "";

        internal void Update(Cadastro novoCadastro)
        {
            this.Nome = novoCadastro.Nome;
            this.Email = novoCadastro.Email;
            this.Telefone = novoCadastro.Telefone;
            this.Endereco = novoCadastro.Endereco;
            this.Complemento = novoCadastro.Complemento;
            this.Bairro = novoCadastro.Bairro;
            this.Municipio = novoCadastro.Municipio;
            this.UF = novoCadastro.UF;
            this.CEP = novoCadastro.CEP;

        }
    }

    [DataContract]
    public class ItemPedido : BaseModel
    {
        [Required]
        [DataMember]
        public Pedido Pedido { get; private set; }
        [Required]
        [DataMember]
        public Produto Produto { get; private set; }
        [Required]
        [DataMember]
        public int Quantidade { get; private set; }
        [Required]
        [DataMember]
        public decimal PrecoUnitario { get; private set; }
        [DataMember]
        public decimal Subtotal => (Quantidade * PrecoUnitario);

        public ItemPedido()
        {

        }

        public ItemPedido(Pedido pedido, Produto produto, int quantidade, decimal precoUnitario)
        {
            Pedido = pedido;
            Produto = produto;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
        }

        public void AtualizaQuantidade(int quantidade)
        {
            this.Quantidade = quantidade;
        }
    }

    public class Pedido : BaseModel
    {
        public Pedido()
        {
            Cadastro = new Cadastro();
        }

        public Pedido(Cadastro cadastro)
        {
            Cadastro = cadastro;
        }

        public List<ItemPedido> Itens { get; private set; } = new List<ItemPedido>();
        [Required]
        public virtual Cadastro Cadastro { get; private set; }
    }

    public class Categoria : BaseModel
    {
        public Categoria()
        {

        }

        public Categoria(string nome)
        {
            this.Nome = nome;
        }

        [Required]
        public string Nome { get; private set; }
    }
}

