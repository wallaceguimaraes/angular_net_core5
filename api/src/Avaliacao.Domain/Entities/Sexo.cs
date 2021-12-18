using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avaliacao.Domain.Entities
{
    public class Sexo
    {
        public Sexo(int sexoId, string descricao) 
        {
            this.SexoId = sexoId;
            this.Descricao = descricao;
   
        }
        public int SexoId { get; set; }

        public string Descricao { get; set; }

        public ICollection<Usuario> Usuarios { get; set; }
    }
}