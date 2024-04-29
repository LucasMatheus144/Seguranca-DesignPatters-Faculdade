using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoPotencial.Areas.Cadastro.Models.Financeiro
{
    [Table("produtos", Schema = "financeiro")]
    public class produtos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int produtos_id { get; set; }

        [Required(ErrorMessage = "Informe a descrição do Produto")]
        [Display(Name = "Descrição")]
        [MaxLength(50)]
        public string produtos_descricao { get; set; }

        [Required(ErrorMessage = "Informe qual a unidade")]
        [Display(Name = "Unidade Produto")]
        [MaxLength(5)]
        public string produtos_unidade { get; set; }

        [Required(ErrorMessage = "Informe a quantidade unitaria")]
        [Display(Name = "Quantidade Unitario")]
        public int produtos_qtde_unit { get; set; }

        [Required(ErrorMessage = "Informe se o produtoe é de um evento")]
        [Display(Name = "Produto Evento")]
        [MaxLength(1)]
        public string produtos_evento { get; set; }
    }
}
//dotnet aspnet-codegenerator controller -name ProdutosController -m produtos -dc ApaDbContext --relativeFolderPath Areas\Cadastro\Controllers\Financeiro --useDefaultLayout --referenceScriptLibraries
