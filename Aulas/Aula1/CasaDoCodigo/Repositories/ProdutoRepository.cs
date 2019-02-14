using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        private readonly ICategoriaRepository categoriaRepository;
        public ProdutoRepository(ApplicationContext contexto
            , ICategoriaRepository categoriaRepository) : base(contexto)
        {
            this.categoriaRepository = categoriaRepository;
        }

        public IList<Produto> GetProdutos()
        {
            return dbSet
                .Include(p => p.Categoria)
                .ToList();
        }

        //Método de busca de produtos pelo nome ou categoria
        public IList<Produto> GetProdutos(string pesquisa)
        {
            return dbSet
                 .Include(p => p.Categoria)
                 .Where(p => p.Nome.Contains(pesquisa) || p.Categoria.Nome.Contains(pesquisa))
                 .ToList();
        }

        public void SaveProdutos(List<Livro> livros)
        {
            foreach (var livro in livros)
            {
                if (!dbSet.Where(p => p.Codigo == livro.Codigo).Any())
                {
                    categoriaRepository.SaveCategoria(livro.Categoria);
                    dbSet.Add(new Produto(livro.Codigo, livro.Nome, livro.Preco
                        , categoriaRepository.GetCategoria(livro.Categoria), livro.Src));
                }
            }
            contexto.SaveChanges();
        }
    }

    public class Livro
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Src { get; set; }
        public decimal Preco { get; set; }
    }
}
