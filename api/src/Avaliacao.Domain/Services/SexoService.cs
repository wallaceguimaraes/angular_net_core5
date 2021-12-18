using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avaliacao.Domain.Entities;
using Avaliacao.Domain.Interfaces.Repositories;
using Avaliacao.Domain.Interfaces.Services;

namespace Avaliacao.Domain.Services
{
    public class SexoService : ISexoService
    {
        private readonly ISexoRepo _sexoRepo;
        public SexoService(ISexoRepo sexoRepo){
            _sexoRepo = sexoRepo;
        }

        public async Task<Sexo> AdicionarSexo(Sexo model)
        {
            
            if(await _sexoRepo.PegaPorIdAsync(model.SexoId) == null){
                _sexoRepo.Adicionar(model);
                if(await _sexoRepo.SalvarMudancasAsync())
                    return model;
            }

            return null;
        }

        public async Task<Sexo[]> PegarTodosSexosAsync()
        {
            try
            {
                var sexos = await _sexoRepo.PegaTodosAsync(); 
                    if(sexos == null) return null;

                    return sexos;
            }
            catch (System.Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }
    }
}