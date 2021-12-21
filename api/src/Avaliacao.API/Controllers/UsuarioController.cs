using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Avaliacao.Domain.Entities;
using Avaliacao.Data.Context;
using Avaliacao.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace Avaliacao.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                 var usuarios = await _usuarioService.PegarTodosUsuariosAsync();
                 if(usuarios == null) return NoContent();

                 return Ok(usuarios);
            }
            catch (System.Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar recuperar usuários. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
             try
            {
                 var usuario = await _usuarioService.PegarUsuarioPorIdAsync(id);
                 if(usuario == null) return NoContent();

                 return Ok(usuario);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar recuperar o usuário com id: {id}. Erro: {ex.Message}");
            }
        }

        [HttpPost("GetByName")]
        public async Task<IActionResult> GetByName(Usuario model)
        {
             try
            {
                 var usuario = await _usuarioService.PegarUsuarioPorNome(model);
                 if(usuario == null) return NoContent();

                 return Ok(usuario);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar recuperar o usuário. Erro: {ex.Message}");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post(Usuario model)
        {

                try
            {
                 var usuario = await _usuarioService.AdicionarUsuario(model);
                 if(usuario == null) return NoContent();

                 return Ok(usuario);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar adicionar o usuário {model.Nome}. Erro: {ex.Message}");
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Usuario model)
        {

            try
            {
                 if(model.UsuarioId != id)
                    this.StatusCode(StatusCodes.Status409Conflict,
                    $"Você está tentando atualizar um usuário errado!");

                 var usuario = await _usuarioService.AtualizarUsuario(model);
                 if(usuario == null) return NoContent();

                 return Ok(usuario);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar atualizar o usuário {model.Nome}. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
             try
            {
                var usuario = await _usuarioService.PegarUsuarioPorIdAsync( id );
                if(usuario == null)
                    this.StatusCode(StatusCodes.Status409Conflict,
                    $"Você está tentando deletar um usuário errado!");

                 if(await _usuarioService.DeletarUsuario(id)){
                     return Ok( new { message = "Deletado!" });
                 }
                 return BadRequest("Ocorreu um problema específico ao deletar o usuário!");
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar atualizar o usuário. Erro: {ex.Message}");
            } 
        }



    }
}