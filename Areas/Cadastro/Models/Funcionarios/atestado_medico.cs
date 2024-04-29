using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoPotencial.Areas.Cadastro.Models.Funcionarios
{
    [Table("atestado_medico", Schema = "funcionario")]
    public class atestado_medico
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o Funcionario")]
        public int funcionario_id { get; set; }

        [Required(ErrorMessage = "Informe o cid")]
        public int cid_id { get; set; }

        [Required(ErrorMessage = "Informe a data do atestado")]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        [StringLength(3)]
        public string Dias { get; set; }

        [ForeignKey("cid_id")]
        public cid Cid { get; set; }

        [ForeignKey("funcionario_id")]
        public funcionario funcionario { get; set; }
    }
}

//dotnet aspnet-codegenerator controller -name AtestadoMedicoController -m atestado_medico -dc ApaDbContext --relativeFolderPath Areas\Cadastro\Controllers --useDefaultLayout --referenceScriptLibraries
