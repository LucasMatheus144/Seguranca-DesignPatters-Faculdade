using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EspacoPotencial.Areas.Cadastro.Models.Financeiro;
using EspacoPotencial.Context;
using EspacoPotencial.master.Areas.Cadastro.Models.Estoque;

namespace EspacoPotencial.Areas.Cadastro.Models.Financeiro
{
    
    [Table("item", Schema = "estoque")]
    public class item
    {   
        
        [Key]
        public int id { get; set; }

        [Display(Name = "Estoque")]
        public int estoque_id { get; set; }

        [Required(ErrorMessage = "Informe a descricao")]
        [StringLength(50)]
        [Display(Name ="Nome Item")]
        public string descricao { get; set; }

        [Required(ErrorMessage = "Informe a quantidade")]
        [Range(0, 999, ErrorMessage = "O valor deve estar entre 0 e 999")]
        public int qntdeunitario { get; set; }

        [MaxLength(2)]
        public decimal? valorunitario { get; set; }

        [Required(ErrorMessage = "Informe a situacao")]
        [MaxLength(1)]
        public string situacao {get ;set;}

        public int tipo_armazem {get ; set;}

        [ForeignKey("estoque_id")]
        public armazem armazem {get ; set;}

        [ForeignKey("tipo_armazem")]
        public tipo tipo { get; set; }

        private StateItem _state;

        public item()
        {
            _state = new ValidItemState(); // Inicialmente, o estado é válido
        }

        public async Task<bool> ValidateItemAsync(ApaDbContext context)
        {
            return await _state.ValidateItemAsync(this, context);
        }

        public void SetState(StateItem state)
        {
            _state = state;
        }
    }
}
//dotnet aspnet-codegenerator controller -name ItemController -m item -dc ApaDbContext --relativeFolderPath Areas\Cadastro\Controllers\Estoque --useDefaultLayout --referenceScriptLibraries
