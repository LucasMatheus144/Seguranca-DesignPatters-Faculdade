using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EspacoPotencial.Areas.Cadastro.Models.Funcionarios;
using EspacoPotencial.Areas.Cadastro.Models.Usuarios;

namespace EspacoPotencial.Areas.Cadastro.Models.Financeiro
{
    [Table("movimentacao_item", Schema = "estoque")]
    public class movimentacao
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "Item")]
        public int item_id {get ; set;}
        
        [Display(Name = "Usuario")]
        public int usuario_id {get; set;}

        [DataType(DataType.Date)]
        [Display(Name = "Data Retirada")]
        [Required(ErrorMessage = "Informe a data retirada")]
        public DateTime data_retirada {get ; set;}

        [DataType(DataType.Date)]
        [Display(Name = "Previsão")]
        [Required(ErrorMessage = "Informe a previsão")]
        public DateTime previsao_devolvido {get ; set;}

        [Display(Name = "Data Devolvido")]
        public DateTime? devolvido {get ;set;}

        [Display(Name = "Descrição")]
        public string descricao {get ;set;}

        [ForeignKey("usuario_id")]
        public usuario usuario {get ; set;}

        [ForeignKey("item_id")]
        public item item {get ; set;}
       
    }
}
//dotnet aspnet-codegenerator controller -name MovimentacaoItemController -m movimentacao -dc ApaDbContext --relativeFolderPath Areas\Cadastro\Controllers\Estoque --useDefaultLayout --referenceScriptLibraries
