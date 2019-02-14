using CasaDoCodigo.Models;
using CasaDoCodigo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public interface ICategoriaRepository
    {
        Categoria GetCategoria(string Nome);
        void SaveCategoria(string Nome);
    }

    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public Categoria GetCategoria(string nomeCategoria)
        {
            var categoria = 
                dbSet
                .Where(c => c.Nome == nomeCategoria)
                .SingleOrDefault();
            return categoria;
        }

        public void SaveCategoria(string Nome)
        {
            if (!dbSet.Where(c => c.Nome == Nome).Any())
            {
                var categoria = dbSet.Add(new Categoria(Nome));
            }
            contexto.SaveChanges();
        }
    }
}
