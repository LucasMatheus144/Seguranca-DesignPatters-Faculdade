using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoPotencial.Areas.Cadastro.Models.Funcionarios
{
    [Table("centro_custo", Schema = "funcionario")]
    public class centro_custo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Informe o nome do Centro Custo")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }
    }
}

//dotnet aspnet-codegenerator controller -name CentroCustoController -m centro_custo -dc ApaDbContext --relativeFolderPath Areas\Cadastro\Controllers --useDefaultLayout --referenceScriptLibraries
