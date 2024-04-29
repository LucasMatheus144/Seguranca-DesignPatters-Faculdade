using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoPotencial.Areas.Cadastro.Models.Financeiro
{
    [Table("movimento_produtos", Schema = "financeiro")]
    public class movimento_produtos
    {
        [Key]
        [Column("movimento_id")]
        public int movimento_id { get; set; }

        public int produto_geral_id { get; set; }

        public int produto_evento_id { get; set; }

        [Column("produtos_qntde")]
        public int ProdutosQuantidade { get; set; }

        [Column("produtos_vl_unit")]
        public decimal ProdutosValorUnitario { get; set; }

        [Column("produtos_vl_total")]
        public decimal ProdutosValorTotal { get; set; }

        [ForeignKey("movimento_id")]
        public movimento_venda MovimentoVenda { get; set; }

        [ForeignKey("produto_geral_id")]
        public produtos_geral ProdutoGeral { get; set; }

        [ForeignKey("produto_evento_id")]
        public produtos_evento ProdutoEvento { get; set; }
    }
}
//dotnet aspnet-codegenerator controller -name MovimentosProdutosController -m movimento_produtos -dc ApaDbContext --relativeFolderPath Areas\Cadastro\Controllers\Financeiro --useDefaultLayout --referenceScriptLibraries
