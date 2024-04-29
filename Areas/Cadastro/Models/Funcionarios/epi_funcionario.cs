using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoPotencial.Areas.Cadastro.Models.Funcionarios
{
    [Table("epi_funcionario", Schema = "funcionario")]
    public class epi_funcionario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o ID do funcionário")]
        public int funcionario_id { get; set; }

        [Required(ErrorMessage = "Informe o ID do EPI geral")]
        public int epi_geral_id { get; set; }

        [Display(Name = "Data Formação")]
        [DataType(DataType.Date)]
        public DateTime Vencimento { get; set; }

        [Display(Name = "Data Formação")]
        [DataType(DataType.Date)]
        public DateTime Entrega { get; set; }

        [ForeignKey("funcionario_id")]
        public funcionario Funcionario { get; set; }

        [ForeignKey("epi_geral_id")]
        public epi epi_geral { get; set; }
    }
}

//dotnet aspnet-codegenerator controller -name EpiFuncionarioController -m epi_funcionario -dc ApaDbContext --relativeFolderPath Areas\Cadastro\Controllers --useDefaultLayout --referenceScriptLibraries
