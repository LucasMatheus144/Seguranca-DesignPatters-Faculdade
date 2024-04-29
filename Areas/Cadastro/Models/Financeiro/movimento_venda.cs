using EspacoPotencial.Areas.Cadastro.Models.Public;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoPotencial.Areas.Cadastro.Models.Financeiro
{
    [Table("movimento_venda", Schema = "financeiro")]
    public class movimento_venda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int movimento_id { get; set; }

        [Required(ErrorMessage = "Informe Funcionario")]
        [Display(Name = "Nome")]
        public int geral_id { get; set; }
        public string geral_tipo { get; set; }

        [MaxLength(1)]
        [Required(ErrorMessage = "Informe O tipo do movimento")]
        [Display(Name = "Tipo")]
        public string movimento_tipo { get; set; }

        [MaxLength(40)]
        public string movimento_outro { get; set; }

        [Required(ErrorMessage = "Informe as pareclas")]
        [Display(Name = "Parcelas")]
        [Range(1, 12, ErrorMessage = "As parcelas devem ser de 1 até 12 vezes")]
        public int movimento_parcelas { get; set; }

        [Required(ErrorMessage = "Informe o dia vencimento")]
        [Display(Name = "Dia Vencimento")]
        [Range(1, 31, ErrorMessage = "O dia de vencimento deve ser entre 1 e 31")]
        public int movimento_dia_vencto { get; set; }

        [Required(ErrorMessage = "Informe a data do movimento")]
        [Display(Name = "Data Movimento")]
        [DataType(DataType.Date)]
        public DateTime movimento_data { get; set; }

        [Required(ErrorMessage = "Informe o valor do movimento")]
        [Display(Name = "Valor")]
        public decimal movimento_valor { get; set; }

        [MaxLength(1)]
        [Required(ErrorMessage = "Informe O tipo do pagamento")]
        [Display(Name = "Pagamento")]
        public string movimento_tipo_pagto { get; set; }

        [MaxLength(40)]
        [Required(ErrorMessage = "Informe o e-mail do responsavel")]
        [Display(Name = "Email")]
        public string movimento_email { get; set; }

        
        [Display(Name = "Data Estorno")]
        [DataType(DataType.Date)]
        public DateTime? movimento_data_estorno { get; set; }

        [ForeignKey("geral_id")]
        public geral Geral { get; set; }

    }
}
//dotnet aspnet-codegenerator controller -name MovimentoVendaController -m movimento_venda -dc ApaDbContext --relativeFolderPath Areas\Cadastro\Controllers\Financeiro --useDefaultLayout --referenceScriptLibraries
