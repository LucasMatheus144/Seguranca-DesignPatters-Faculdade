using EspacoPotencial.Areas.Cadastro.Models.Funcionarios;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoPotencial.Areas.Cadastro.Models.Usuarios
{
    [Table("comorbidade_usuario", Schema = "usuarios")]
    public class comorbidade_usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o usuario")]
        [Display(Name = "Usuario")]
        public int usuario_id { get; set; }

        [ForeignKey("usuario_id")]
        public usuario Usuario { get; set; }

        [Required(ErrorMessage = "Informe a descricao")]
        [Display(Name = "Comorbidade")]
        public int comorbidade_id { get; set; }

        [ForeignKey("comorbidade_id")]
        public comorbidade Comorbidade { get; set; }
    }
}
//dotnet aspnet-codegenerator controller -name ComorbidadeUsuarioController -m comorbidade_usuario -dc ApaDbContext --relativeFolderPath  Areas\Cadastro\Controllers\Usuarios --useDefaultLayout --referenceScriptLibraries
