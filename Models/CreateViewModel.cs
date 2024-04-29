using EspacoPotencial.Areas.Cadastro.Models;
using EspacoPotencial.Areas.Cadastro.Models.Funcionarios;
using EspacoPotencial.Areas.Cadastro.Models.Public;
using EspacoPotencial.Areas.Cadastro.Models.Usuarios;
using System.Collections.Generic;


namespace EspacoPotencial.Models
{
    public class CreateViewModel
    {
        public geral GeralModel { get; set; } = new geral();

        public funcionario FuncionarioModel { get; set; }
        public usuario UsuarioModel { get; set; }
        public List<Exibir> Resultados { get; set; } = new List<Exibir>();

        public class Exibir
        {
            public int GeralId { get; set; }
            public string Nome { get; set; }
            public byte[] Foto { get; set; }
            public string Email { get; set; }
            public string Tipo {get; set;}
        }


    }
}

//dotnet aspnet-codegenerator controller -name GeralController -m geral -dc ApaDbContext --relativeFolderPath Areas\Cadastro\Controllers --useDefaultLayout --referenceScriptLibraries
