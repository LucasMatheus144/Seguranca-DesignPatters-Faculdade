using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoPotencial.Areas.Cadastro.Models.Usuarios
{
    [Table("desligamento_motivos_usuario", Schema = "usuarios")]
    public class desligamento_motivos_usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Informe o motivo desligamento")]
        public string Motivo { get; set; }
    }
}
//dotnet aspnet-codegenerator controller -name MotivoDesligamentoUsuarioController -m desligamento_motivos_usuario -dc ApaDbContext --relativeFolderPath  Areas\Cadastro\Controllers\Usuarios --useDefaultLayout --referenceScriptLibraries
