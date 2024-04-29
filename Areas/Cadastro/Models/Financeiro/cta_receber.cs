using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoPotencial.Areas.Cadastro.Models.Financeiro
{
    [Table("cta_receber", Schema = "financeiro")]
    public class cta_receber
    {
        [Key]
        [Column("cta_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContaReceberId { get; set; }

        [Column("movimento_id")]
        public int MovimentoId { get; set; }

        [MaxLength(1)]
        [Column("movimento_tipo")]
        public char MovimentoTipo { get; set; }

        [Column("movimento_parcelas")]
        public int MovimentoParcelas { get; set; }

        [Column("cta_pagto")]
        public DateTime CtaPagamento { get; set; }

        [Column("cta_valor")]
        public decimal CtaValor { get; set; }

        [Column("cta_multa")]
        public decimal CtaMulta { get; set; }

        [Column("cta_juros")]
        public decimal CtaJuros { get; set; }

        [Column("cta_receber_baixa")]
        public int CtaReceberBaixa { get; set; }

        [ForeignKey("movimento_id")]
        public movimento_venda MovimentoVenda { get; set; }

        [ForeignKey("movimento_tipo")]
        public movimento_venda MovimentoVenda_tipo { get; set; }

        [ForeignKey("movimento_parcelas")]
        public movimento_venda MovimentoVenda_parcela { get; set; }
    }
}
//dotnet aspnet-codegenerator controller -name CTAReceberController -m cta_receber -dc ApaDbContext --relativeFolderPath Areas\Cadastro\Controllers\Financeiro --useDefaultLayout --referenceScriptLibraries
