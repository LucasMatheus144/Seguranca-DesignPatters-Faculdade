using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoPotencial.Areas.Cadastro.Models.Financeiro
{
    [Table("eventos", Schema = "financeiro")]
    public class eventos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int eventos_id { get; set; }

        [Required(ErrorMessage = "Informe o nome do Evento")]
        [Display(Name = "Nome")]
        [MaxLength(50)]
        public string evento_nome { get; set; }

        [Required(ErrorMessage = "Informe a data inicio do evento")]
        [Display(Name = "Data Inicio")]
         [DataType(DataType.Date)]
        public DateTime evento_data_ini { get; set; }

        [Required(ErrorMessage = "Informe a data do final do evento")]
        [Display(Name = "Data Final")]
        [DataType(DataType.Date)]
        public DateTime evento_data_fin { get; set; }
    }
}
//dotnet aspnet-codegenerator controller -name EventosController -m eventos -dc ApaDbContext --relativeFolderPath Areas\Cadastro\Controllers\Financeiro --useDefaultLayout --referenceScriptLibraries
