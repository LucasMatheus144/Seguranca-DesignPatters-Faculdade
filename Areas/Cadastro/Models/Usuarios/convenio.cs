using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoPotencial.Areas.Cadastro.Models.Usuarios
{
    [Table("convenio", Schema = "usuarios")]
    public class convenio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Informe o nome do convenio")]
        public string Nome { get; set; }
    }
}
//dotnet aspnet-codegenerator controller -name ConvenioController -m convenio -dc ApaDbContext --relativeFolderPath  Areas\Cadastro\Controllers\Usuarios --useDefaultLayout --referenceScriptLibraries
