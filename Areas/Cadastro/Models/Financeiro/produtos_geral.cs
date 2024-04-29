using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoPotencial.Areas.Cadastro.Models.Financeiro
{
    [Table("produtos_geral", Schema = "financeiro")]
    public class produtos_geral
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int produto_geral_id { get; set; }

        [Required(ErrorMessage = "Informe o grupo permitido")]
        [Display(Name = "Grupo")]
        public int grp_id { get; set; }

        [Required(ErrorMessage = "Informe o produtos")]
        [Display(Name = "Produtos")]
        public int produto_id { get; set; }

        [Required(ErrorMessage = "Informe valor unitario")]
        [Display(Name = "Valor Unitario")]
        public decimal produto_geral_valor_uni { get; set; }

        [ForeignKey("grp_id")]
        public grupo_permitido GrupoPermitido { get; set; }

        [ForeignKey("produto_id")]
        public produtos Produto { get; set; }
    }
}
//dotnet aspnet-codegenerator controller -name ProdutosGeralController -m produtos_geral -dc ApaDbContext --relativeFolderPath Areas\Cadastro\Controllers\Financeiro --useDefaultLayout --referenceScriptLibraries
