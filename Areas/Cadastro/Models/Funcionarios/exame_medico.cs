using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoPotencial.Areas.Cadastro.Models.Funcionarios
{
    [Table("exame_medico", Schema = "funcionario")]
    public class exame_medico
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o ID do funcionário")]
        public int funcionario_id { get; set; }

        [StringLength(1)]
        public string Exame { get; set; }

        [Display(Name = "Data Agendamento")]
        [DataType(DataType.Date)]
        public DateTime DataAgendada { get; set; }

        [Display(Name = "Data Execução")]
        [DataType(DataType.Date)]
        public DateTime? DataExecuta { get; set; }

        [ForeignKey("funcionario_id")]
        public funcionario Funcionario { get; set; }
    }
}

//dotnet aspnet-codegenerator controller -name ExameMedicoController -m exame_medico -dc ApaDbContext --relativeFolderPath Areas\Cadastro\Controllers --useDefaultLayout --referenceScriptLibraries
