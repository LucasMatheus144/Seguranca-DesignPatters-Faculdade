using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EspacoPotencial.Areas.Cadastro.Models.Funcionarios;

namespace EspacoPotencial.Areas.Cadastro.Models.Financeiro
{
    [Table("armazem", Schema = "estoque")]
    public class armazem
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "Funcionario Responsavel")]
        public int funcionario_id { get; set; }

        [Display(Name = "Nome Armazem")]
        [Required(ErrorMessage = "Informe o nome")]
        [StringLength(20)]
        public string nome { get; set; }
        
        [NotMapped]
        public DateTime datacadastro { get; set; }

        [Display(Name = "Limite")]
        [Required(ErrorMessage = "Informe o nome")]
        [Range(0, 999, ErrorMessage = "O valor deve estar entre 0 e 999")]
        public int qntdelimite { get; set; }

        [Display(Name = "Tipo Armazem")]
        public int tipo_armazem { get; set; }

        [ForeignKey("funcionario_id")]
        public funcionario funcionario { get; set; }

        [ForeignKey("tipo_armazem")]
        public tipo tipo { get; set; }
    }
}
//dotnet aspnet-codegenerator controller -name ArmazemController -m armazem -dc ApaDbContext --relativeFolderPath Areas\Cadastro\Controllers\Estoque --useDefaultLayout --referenceScriptLibraries
