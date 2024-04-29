using EspacoPotencial.Areas.Cadastro.Models;
using EspacoPotencial.Areas.Cadastro.Models.Funcionarios;
using EspacoPotencial.Areas.Cadastro.Models.Public;
using EspacoPotencial.Areas.Cadastro.Models.Usuarios;
using System.Collections.Generic;


namespace EspacoPotencial.Models
{
    public class SalaAulaModel
    {
        public geral Geral {get; set;}
        public usuario Usuario { get; set; }
        public funcionario Funcionario {get ; set;}
        public sala Salas {get; set;}

        public salas_diario Diario {get; set;}

        public usuario_sala UsuSala {get; set;}

        public frequencia Frequencia {get ; set;}

        public calendario Calendario {get; set;}

        public datas Datas {get; set;}

        public responsavel Responsavel {get ; set;}

        public List<Exibir> Resultados { get; set; } = new List<Exibir>();

         public class Exibir
        {
            public string NomeSala { get; set; }
            public string NomeUsario { get; set; }
            public string NomeProfessor { get; set; }
            public string NomeResponsavel { get; set; }
            public string TelefoneResponsavel { get; set; }
            public string CodPeriodo {get ;set;}
            public int IdSala {get ; set; }

            public int IdUsuario {get ;set;}
           
        }
    
    }
 
}




