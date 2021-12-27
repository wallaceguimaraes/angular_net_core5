using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avaliacao.Domain.Entities;
using Avaliacao.Domain.Interfaces.Repositories;
using Avaliacao.Domain.Interfaces.Services;


namespace Avaliacao.Domain.Services
{
    public class UsuarioService : IUsuarioService
    {

        private readonly IUsuarioRepo _usuarioRepo;

        public UsuarioService(IUsuarioRepo usuarioRepo){
            _usuarioRepo = usuarioRepo;

        }
        public async Task<Usuario> AdicionarUsuario(Usuario model){
           /*  
            if(await _usuarioRepo.PegaPorNome(model.Nome) != null )
                throw new Exception("Já existe um usuário com esse nome!"); */
            
            if(await _usuarioRepo.PegaPorIdAsync(model.UsuarioId) == null){
                _usuarioRepo.Adicionar(model);
                if(await _usuarioRepo.SalvarMudancasAsync())
                    return model;
            }

            return null;
        }

       public async Task<Usuario> AtualizarUsuario(Usuario model){
              
               if(await _usuarioRepo.PegaPorIdAsync(model.UsuarioId) != null){
                _usuarioRepo.Atualizar(model);
                if(await _usuarioRepo.SalvarMudancasAsync())
                    return model;
                }

            return null;

        }

        public async Task<bool> DeletarUsuario(int id){
            var usuario = await _usuarioRepo.PegaPorIdAsync(id);
            if (usuario == null) throw new Exception("O usuário que você tentou inativar não existe!");

            usuario.Ativo = false; 
             _usuarioRepo.Deletar(usuario); 
            return await _usuarioRepo.SalvarMudancasAsync();
            }

       public async Task<bool> InativarAtivarUsuario(Usuario model){
           var usuario = await _usuarioRepo.PegaPorIdAsync(model.UsuarioId);
           if (usuario == null) throw new Exception("O usuário que você tentou inativar não existe!");

           if(usuario.Ativo == true){
           usuario.Ativo = false;    
           } else {
               usuario.Ativo = true;    
           }
               
           _usuarioRepo.Atualizar(usuario);

           return await _usuarioRepo.SalvarMudancasAsync();
        }

       public async Task<Usuario[]> PegarTodosUsuariosAsync(){
            try
            {
                var usuarios = await _usuarioRepo.PegaTodosAsync(); 
                    if(usuarios == null) return null;

                    return usuarios;
            }
            catch (System.Exception ex)
            { 
                throw new Exception(ex.Message);
            }
        }

        public async Task<Usuario[]> PegarUsuarioPorNome(Usuario model){
        
            try
            {
                var usuarios = await _usuarioRepo.PegaPorNome(model); 
                    if(usuarios == null) return null;

                    return usuarios;
            }
            catch (System.Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }   


       public async Task<Usuario> PegarUsuarioPorIdAsync(int usuarioId){
        
            try
            {
                var usuario = await _usuarioRepo.PegaPorIdAsync(usuarioId); 
                    if(usuario == null) return null;

                    return usuario;
            }
            catch (System.Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

    }

}