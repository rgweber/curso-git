using CasaDoCodigo.Models;
using CasaDoCodigo.Models.ViewModels;
using CasaDoCodigo.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IProdutoRepository produtoRepository;
        private readonly IPedidoRepository pedidoRepository;
        private readonly IItemPedidoRepository itemPedidoRepository;

        public PedidoController(IProdutoRepository produtoRepository,
            IPedidoRepository pedidoRepository,
            IItemPedidoRepository itemPedidoRepository)
        {
            this.produtoRepository = produtoRepository;
            this.pedidoRepository = pedidoRepository;
            this.itemPedidoRepository = itemPedidoRepository;
        }

        //Busca de Produtos
        public IActionResult BuscaDeProdutos(string pesquisa)
        {
            IList<Produto> produtos;
            if (String.IsNullOrEmpty(pesquisa))
            {
                produtos = produtoRepository.GetProdutos();
            }
            else
            {
                produtos = produtoRepository.GetProdutos(pesquisa);
            }
            BuscaProdutoViewModel buscaProdutoViewModel = new BuscaProdutoViewModel(produtos);

            return base.View(buscaProdutoViewModel);
        }


        public IActionResult Carrossel()
        {
            return View(produtoRepository.GetProdutos());
        }

        public IActionResult Carrinho(string codigo)
        {
            if (!string.IsNullOrEmpty(codigo))
            {
                pedidoRepository.AddItem(codigo);
            }

            List<ItemPedido> itens = pedidoRepository.GetPedido().Itens;
            CarrinhoViewModel carrinhoViewModel = new CarrinhoViewModel(itens);
            return base.View(carrinhoViewModel);
        }

        public IActionResult Cadastro()
        {
            var pedido = pedidoRepository.GetPedido();
            if (pedido == null)
            {
                return RedirectToAction("Carrossel");  
            }
            return View(pedido.Cadastro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Resumo(Cadastro cadastro)
        {
            if(ModelState.IsValid)
            {
                return View(pedidoRepository.UpdateCadastro(cadastro));
            }

            return RedirectToAction("Cadastro");
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public UpdateQuantidadeResponse UpdateQuantidade([FromBody]ItemPedido itemPedido)
        {
            return pedidoRepository.UpdateQuantidade(itemPedido);
        }

        [HttpPost]
        public UpdateQuantidadeResponse RemoveItemPedido([FromBody] ItemPedido itemPedido)
        {
            return pedidoRepository.RemoveItemPedido(itemPedido);
        }
    }
}
