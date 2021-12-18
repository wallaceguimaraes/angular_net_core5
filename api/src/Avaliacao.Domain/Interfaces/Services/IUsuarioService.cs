using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avaliacao.Domain.Entities;

namespace Avaliacao.Domain.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<Usuario> AtualizarUsuario(Usuario model);

        Task<Usuario> AdicionarUsuario(Usuario model);

        Task<bool> DeletarUsuario(int usuarioId);
        
        Task<Usuario[]> PegarTodosUsuariosAsync();

        Task<Usuario> PegarUsuarioPorIdAsync(int usuarioId);

        Task<Usuario> PegarUsuarioPorNome(Usuario usuario);

    }
}