using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoPotencial.Areas.Cadastro.Models.Funcionarios
{
    [Table("cargo_funcionario", Schema = "funcionario")]
    public class cargo_funcionario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o funcionario")]
        [Display(Name = "Nome Funcionario")]
        public int funcionario_id { get; set; }

        [Required(ErrorMessage = "Informe o cargo")]
        [Display(Name = "Cargo")]
        public int cargos_id { get; set; }

        [Required(ErrorMessage = "Informe a situação")]
        [StringLength(1)]
        public string Situacao { get; set; }

        [Display(Name = "Motivo Desligamento")]
        public int? motivo_id { get; set; }

        [Display(Name = "Data Inicio")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Informe a data inicial")]
        [Column(TypeName = "timestamp")]
        public DateTime DataInicial { get; set; }

        [Display(Name = "Data Fim")]
        [DataType(DataType.Date)]
        [Column(TypeName = "timestamp")]
        public DateTime? DataFinal { get; set; }

        public string Observacao { get; set; }

        [ForeignKey("cargos_id")]
        public cargos cargos { get; set; }

        [ForeignKey("motivo_id")]
        public motivo_desligamento motivo_desligamento { get; set; }

        [ForeignKey("funcionario_id")]
        public funcionario funcionario {get ; set;}
    }
}

//dotnet aspnet-codegenerator controller -name CargoFuncionarioController -m cargo_funcionario -dc ApaDbContext --relativeFolderPath Areas\Cadastro\Controllers --useDefaultLayout --referenceScriptLibraries
