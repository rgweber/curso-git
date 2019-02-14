using CasaDoCodigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public interface ICadastroRepository
    {
        Cadastro UpdateCadastro(int CadastroId, Cadastro cadastro);
    }

    public class CadastroRepository : BaseRepository<Cadastro>, ICadastroRepository
    {
        public CadastroRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public Cadastro UpdateCadastro(int CadastroId, Cadastro cadastro)
        {
            var cadastroDB = dbSet.
                Where(c => c.Id == CadastroId)
                .SingleOrDefault();

            if (cadastroDB == null)
            {
                throw new ArgumentException("cadastro");
            }

            cadastroDB.Update(cadastro);
            contexto.SaveChanges();
            return cadastroDB;
        }
    }
}
