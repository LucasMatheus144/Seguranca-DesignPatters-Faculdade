using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoPotencial.Areas.Cadastro.Models.Funcionarios
{
    [Table("motivo_desligamento", Schema = "funcionario")]
    public class motivo_desligamento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o motivo do desligamento")]
        [Display(Name = "Descrição do Motivo Desligamento")]
        public string Descricao { get; set; }
    }
}

//dotnet aspnet-codegenerator controller -name MotivoDesligamentoController -m motivo_desligamento -dc ApaDbContext --relativeFolderPath Areas\Cadastro\Controllers\Funcionario --useDefaultLayout --referenceScriptLibraries
