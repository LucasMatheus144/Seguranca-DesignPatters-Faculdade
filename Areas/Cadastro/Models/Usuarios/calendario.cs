using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoPotencial.Areas.Cadastro.Models.Usuarios
{
    [Table("calendario", Schema = "usuarios")]
    public class calendario
    {
        [Key]
        [Column("calendario_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe a sala")]
        [Display(Name = "Sala")]
        public int sala_id { get; set; }

        [Required(ErrorMessage = "Informe a data")]
        [Display(Name = "Evento Data")]
        public int datas_id { get; set; }

        [ForeignKey("datas_id")]
        public datas Datas { get; set; }

         [ForeignKey("sala_id")]
        public sala Salas {get; set;}
    }
}
//dotnet aspnet-codegenerator controller -name CalendarioController -m calendario -dc ApaDbContext --relativeFolderPath Areas\Cadastro\Controllers\Usuarios --useDefaultLayout --referenceScriptLibraries
