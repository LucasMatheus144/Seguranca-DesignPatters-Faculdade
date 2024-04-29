using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EspacoPotencial.Areas.Cadastro.Models.Funcionarios;

namespace EspacoPotencial.Areas.Cadastro.Models.Financeiro
{
    [Table("tipo", Schema = "estoque")]
    public class tipo
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Informe o nome")]
        public string nome { get; set; }

        [Required(ErrorMessage = "Informe a descricao")]
        public string descricao { get; set; }

    }
}
//dotnet aspnet-codegenerator controller -name TipoController -m tipo -dc ApaDbContext --relativeFolderPath Areas\Cadastro\Controllers\Estoque --useDefaultLayout --referenceScriptLibraries
