using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoPotencial.Areas.Cadastro.Models.Financeiro
{
    [Table("produtos_evento", Schema = "financeiro")]
    public class produtos_evento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int prod_evento_id { get; set; }

        [Column("eventos_id")]
        [Required(ErrorMessage = "Informe o Evento")]
        [Display(Name = "Evento")]
        public int eventos_id { get; set; }

        [Column("grp_id")]
        [Required(ErrorMessage = "Informe o grupo")]
        [Display(Name = "Grupo ")]
        public int grp_id { get; set; }

        [Required(ErrorMessage = "Informe o produto")]
        [Display(Name = "Produto")]
        public int produtos_id { get; set; }

        [Required(ErrorMessage = "Informe valor unitario")]
        [Display(Name = "Valor")]
        public decimal prod_evento_valor_uni { get; set; }

        [ForeignKey("eventos_id")]
        public eventos Evento { get; set; }

        [ForeignKey("grp_id")]
        public grupo_permitido GrupoPermitido { get; set; }

        [ForeignKey("produtos_id")]
        public produtos Produto { get; set; }
    }
}
//dotnet aspnet-codegenerator controller -name ProdutosEventosController -m produtos_evento -dc ApaDbContext --relativeFolderPath Areas\Cadastro\Controllers\Financeiro --useDefaultLayout --referenceScriptLibraries
