using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoPotencial.Areas.Cadastro.Models.Financeiro
{
    [Table("cc", Schema = "financeiro")]
    public class cc
    {
        [Key]
        [Column("cc_id")]
        public int Id { get; set; }

        [Column("cc_venda_id_origem")]
        [MaxLength(1)]
        public char ContaCorrenteVendaIdOrigem { get; set; }

        [Column("cc_venda_id")]
        public int ContaCorrenteVendaId { get; set; }

        [Column("cc_data")]
        public DateTime ContaCorrenteData { get; set; }

        [Column("cc_deb_cred")]
        [MaxLength(1)]
        public char ContaCorrenteDebitoCredito { get; set; }

        [Column("cc_valor")]
        public float ContaCorrenteValor { get; set; }

        [ForeignKey("movimento_id")]
        public movimento_venda MovimentoVenda_id { get; set; }
        [ForeignKey("geral_tipo")]
        public movimento_venda MovimentoVenda_tipo { get; set; }
    }
}
//dotnet aspnet-codegenerator controller -name CCController -m cc -dc ApaDbContext --relativeFolderPath Areas\Cadastro\Controllers\Financeiro --useDefaultLayout --referenceScriptLibraries
