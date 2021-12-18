using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Avaliacao.Domain.Entities;
using Avaliacao.Data.Context;

namespace Avaliacao.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SexoController : ControllerBase
    {
        
         private readonly DataContext _context;

        public SexoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Sexo> Get()
        {
            return _context.Sexos;
        }

        [HttpGet("{id}")]
        public Sexo Get(int id)
        {
            return _context.Sexos.FirstOrDefault( sexo => sexo.SexoId == id ); 
        
        }

        [HttpPost]
        public IEnumerable<Sexo> Post(Sexo sexo)
        {
                _context.Sexos.Add(sexo);

                if ( _context.SaveChanges() > 0 ){
                    return _context.Sexos;
                } 
                else {
                    throw new Exception("Você não conseguiu adicionar um sexo!");
                }

        }
    }
}