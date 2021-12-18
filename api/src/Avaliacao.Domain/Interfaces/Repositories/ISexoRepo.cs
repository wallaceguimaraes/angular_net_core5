using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avaliacao.Domain.Entities;

namespace Avaliacao.Domain.Interfaces.Repositories
{
    public interface ISexoRepo : IGeralRepo
    {

        Task<Sexo[]> PegaTodosAsync();

        Task<Sexo> PegaPorIdAsync(int id);

    }
}