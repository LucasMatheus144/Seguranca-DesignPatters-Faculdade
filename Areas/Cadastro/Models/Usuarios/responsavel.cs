using EspacoPotencial.Areas.Cadastro.Models.Funcionarios;
using EspacoPotencial.Areas.Cadastro.Models.Public;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoPotencial.Areas.Cadastro.Models.Usuarios
{
    [Table("responsavel", Schema = "usuarios")]
    public class responsavel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        
        public int geral_id { get; set; }

        public int? geral_func {get ; set;}

        [Required(ErrorMessage = "Informe o nome")]
        [Display(Name = "Usuario")]
        public int usuario_id { get; set; }

        [Required(ErrorMessage = "Informe o nome")]
        [Display(Name = "Funcionario")]
        public int funcionario_id {get; set; }

        [Required(ErrorMessage = "Vinculo Obrigatorio")]
        [MaxLength(1)]
        public string Vinculo { get; set; }

        [MaxLength(40)]
        [Required(ErrorMessage = "Informe o nome")]
        public string LocalTrabalho { get; set; }

        [MaxLength(1)]
        [Required(ErrorMessage = "Informe o nome")]
        public string Retira { get; set; }

        public string Observacao { get; set; }

        [ForeignKey("geral_id")]
        public geral Geral { get; set; }

        [ForeignKey("usuario_id")]
        public usuario Usuario { get; set; }

        [ForeignKey("funcionario_id")]
        public funcionario Funcionario {get ; set;}

    }
}


//dotnet aspnet-codegenerator controller -name ResponsavelController -m responsavel -dc ApaDbContext --relativeFolderPath  Areas\Cadastro\Controllers\Usuarios --useDefaultLayout --referenceScriptLibraries
