using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoPotencial.Areas.Cadastro.Models.Funcionarios
{
    [Table("cid", Schema = "funcionario")]
    public class cid
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Informe o nome do Cid")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }
    }
}

//dotnet aspnet-codegenerator controller -name CidController -m cid -dc ApaDbContext --relativeFolderPath Areas\Cadastro\Controllers --useDefaultLayout --referenceScriptLibraries
