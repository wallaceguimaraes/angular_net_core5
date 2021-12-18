using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avaliacao.Data.Context;
using Avaliacao.Domain.Entities;
using Avaliacao.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Avaliacao.Data.Repositories
{


    public class SexoRepo : GeralRepo, ISexoRepo
    {
        
        private readonly DataContext _context;

        public SexoRepo(DataContext context) : base(context)
        {
                _context = context;
        }

        public async Task<Sexo> PegaPorIdAsync(int id)
        {
             IQueryable<Sexo> query = _context.Sexos;
            query = query.AsNoTracking()
                         .OrderBy(sexo => sexo.SexoId)
                         .Where(a => a.SexoId == id);
            
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Sexo[]> PegaTodosAsync()
        {
              IQueryable<Sexo> query = _context.Sexos;
            
             query = query.Include(sexo => sexo.Usuarios);
                         
            return await query.ToArrayAsync();
        }
    }
}