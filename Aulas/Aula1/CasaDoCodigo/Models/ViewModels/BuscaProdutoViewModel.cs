
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Models.ViewModels
{
    public class BuscaProdutoViewModel
    {
        public BuscaProdutoViewModel(IList<Produto> produtos)
        {
            this.Produtos = produtos;
        }

        public IList<Produto> Produtos;

        public string Pesquisa;
    }
}
