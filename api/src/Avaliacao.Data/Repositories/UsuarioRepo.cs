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
    public class UsuarioRepo : GeralRepo, IUsuarioRepo
    {
        private readonly DataContext _context;

        public UsuarioRepo(DataContext context) : base(context)
        {
            _context = context;
        }
        
        public Task<Usuario> DeletarLogicamente(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario> PegaPorIdAsync(int id)
        {
            IQueryable<Usuario> query = _context.Usuarios;
            query = query.AsNoTracking()
                         .OrderBy(usuario => usuario.UsuarioId)
                         .Where(a => a.UsuarioId == id)
                         .Include( a => a.Sexo);
            
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Usuario[]> PegaPorNome(Usuario usuario)
        {
            IQueryable<Usuario> query = _context.Usuarios;
            
             query = query.AsNoTracking()
                            .OrderBy(u => usuario.UsuarioId)
                            .Where(u => EF.Functions.Like(u.Nome , '%'+usuario.Nome+'%' ) 
                                        && u.Ativo == usuario.Ativo)
                            .IgnoreQueryFilters()
                            .Include(u => u.Sexo);
            
            return await query.ToArrayAsync();
        }

        public async Task<Usuario[]> PegaTodosAsync()
        {   
            IQueryable<Usuario> query = _context.Usuarios;
            
             query = query.AsNoTracking()
                         .OrderBy(usuario => usuario.UsuarioId)
                         .Include(usuario => usuario.Sexo);
            return await query.ToArrayAsync();
            
        }

    }
}