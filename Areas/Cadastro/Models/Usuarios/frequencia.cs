using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoPotencial.Areas.Cadastro.Models.Usuarios
{
    [Table("frequencia", Schema = "usuarios")]
    public class frequencia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Sala")]
        [Required(ErrorMessage = "Informe a sala de aula")]
        [DataType(DataType.Date)]
        public int sala_id { get; set; }

        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "Informe o Usuario")]
        public int usuario_id { get; set; }

        
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        public Boolean Registro { get; set; }

        public string Observacao { get; set; }

        [ForeignKey("sala_id")]
        public sala Sala {get ;set;}

        [ForeignKey("usuario_id")]
        public usuario_sala UsuarioSala {get ; set;}
    }
}
//dotnet aspnet-codegenerator controller -name FrequenciaController -m frequencia -dc ApaDbContext --relativeFolderPath  Areas\Cadastro\Controllers\Usuarios --useDefaultLayout --referenceScriptLibraries
