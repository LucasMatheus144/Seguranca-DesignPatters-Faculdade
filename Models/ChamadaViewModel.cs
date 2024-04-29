using EspacoPotencial.Areas.Cadastro.Models;
using EspacoPotencial.Areas.Cadastro.Models.Funcionarios;
using EspacoPotencial.Areas.Cadastro.Models.Public;
using EspacoPotencial.Areas.Cadastro.Models.Usuarios;
using System.Collections.Generic;


namespace EspacoPotencial.Models
{
    public class ChamadaViewModel
    {
        public geral Geral {get; set;}
      
        public sala Salas {get; set;}
        public usuario_sala UsuSala {get; set;}

        public frequencia Frequencia {get ; set;}

         public int IdSala { get; set; }
        public int IdUsuario { get; set; }

        public List<Mostrar> Resultados { get; set; } = new List<Mostrar>();

         public class Mostrar
        {
            public int Id { get; set; }
            public string NomeSala { get; set; }
            public string NomeUsario { get; set; }
            public int IdSala {get ;set;}
            public int UsuarioId { get;  set; }
            public string Observacao { get; set; } 
            public Boolean Registro { get; set; } 
        }
    
    }
 
}




