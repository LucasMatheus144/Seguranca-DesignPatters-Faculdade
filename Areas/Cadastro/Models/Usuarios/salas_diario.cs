using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoPotencial.Areas.Cadastro.Models.Usuarios
{
    [Table("salas_diario", Schema = "usuarios")]
    public class salas_diario
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("sala_id")] 
        public sala Sala { get; set; }

        [Column("sala_id")]
        [Required(ErrorMessage = "Informe o nome")]
        [Display(Name = "Sala")]
        public int sala_id { get; set; }

        [MaxLength(1)]
        public string Periodo { get; set; }

        [Required(ErrorMessage = "Informe a nota da aula administrada")]
        [Display(Name = "Descrição")]
        public string Nota { get; set; }
    }
}
//dotnet aspnet-codegenerator controller -name SalaDiarioController -m salas_diario -dc ApaDbContext --relativeFolderPath  Areas\Cadastro\Controllers\Usuarios --useDefaultLayout --referenceScriptLibraries
