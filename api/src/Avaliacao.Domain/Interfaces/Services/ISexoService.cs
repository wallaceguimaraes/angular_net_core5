using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avaliacao.Domain.Entities;

namespace Avaliacao.Domain.Interfaces.Services
{
    public interface ISexoService
    {
        Task<Sexo> AdicionarSexo(Sexo model);

        Task<Sexo[]> PegarTodosSexosAsync();

    }
}