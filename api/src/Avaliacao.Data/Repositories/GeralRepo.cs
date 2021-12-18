using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avaliacao.Data.Context;
using Avaliacao.Domain.Interfaces.Repositories;

namespace Avaliacao.Data.Repositories
{
    public class GeralRepo : IGeralRepo
    {
        private readonly DataContext _context;
        public GeralRepo(DataContext context)
        {
            _context = context;
            
        }
        public void Adicionar<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Atualizar<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Deletar<T>(T entity) where T : class
        {
            _context.Remove(entity);

        }

        public void DeletarVarios<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }

        public async Task<bool> SalvarMudancasAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;

        }
    }
}