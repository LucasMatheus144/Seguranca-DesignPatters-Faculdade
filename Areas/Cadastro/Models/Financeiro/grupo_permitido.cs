using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoPotencial.Areas.Cadastro.Models.Financeiro
{
    [Table("grupo_permitido", Schema = "financeiro")]
    public class grupo_permitido
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int grp_id { get; set; }

        [Required(ErrorMessage = "Informe a descrição do grupo")]
        [Display(Name = "Descrição")]
        [MaxLength(50)]
        public string grp_descricao { get; set; }
    }
}
//dotnet aspnet-codegenerator controller -name GrupoPermitidoController -m grupo_permitido -dc ApaDbContext --relativeFolderPath Areas\Cadastro\Controllers\Financeiro --useDefaultLayout --referenceScriptLibraries
