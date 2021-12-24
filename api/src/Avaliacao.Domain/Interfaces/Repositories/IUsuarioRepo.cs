using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avaliacao.Domain.Entities;

namespace Avaliacao.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepo : IGeralRepo
    {
        Task<Usuario[]> PegaTodosAsync();

        Task<Usuario> PegaPorIdAsync(int id);

        Task<Usuario[]> PegaPorNome(Usuario usuario);

        Task<Usuario> DeletarLogicamente(Usuario usuario);

    }
}