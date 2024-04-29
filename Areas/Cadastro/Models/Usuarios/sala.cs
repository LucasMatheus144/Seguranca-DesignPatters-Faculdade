using EspacoPotencial.Areas.Cadastro.Models.Funcionarios;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoPotencial.Areas.Cadastro.Models.Usuarios
{
    [Table("sala", Schema = "usuarios")]
    public class sala
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(1)]
        [Required(ErrorMessage = "Informe o periodo")]
        public string Periodo { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Informe o nome da sala")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "Informe o professor da Sala")]
        public int funcionario_id { get; set; }

        [ForeignKey("funcionario_id")]
        public funcionario Funcionario { get; set; }
    }
}
//dotnet aspnet-codegenerator controller -name SalaController -m sala -dc ApaDbContext --relativeFolderPath  Areas\Cadastro\Controllers\Usuarios --useDefaultLayout --referenceScriptLibraries
