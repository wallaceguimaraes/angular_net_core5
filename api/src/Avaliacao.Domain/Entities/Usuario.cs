using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avaliacao.Domain.Entities
{
    public class Usuario
    {
        public int UsuarioId { get; set; }

        public string Nome { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Senha { get; set; }

        public Boolean Ativo { get; set; }

        public string Email { get; set; }

        public int SexoId { get; set; }

        public Sexo Sexo { get; set; }
          
        public Usuario() { }

        public Usuario(int id)
        {
          UsuarioId = id;   
        } 

        
         
    }
}