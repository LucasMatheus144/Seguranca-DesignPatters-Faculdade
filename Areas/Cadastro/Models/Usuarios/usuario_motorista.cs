using EspacoPotencial.Areas.Cadastro.Models.Funcionarios;
using EspacoPotencial.Areas.Cadastro.Models.Public;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoPotencial.Areas.Cadastro.Models.Usuarios
{
    [Table("usuario_motorista", Schema = "usuarios")]
    public class usuario_motorista
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Usuario")]
        public int usuario_id { get; set; }

        [Display(Name = "Motorista")]
        public int motorista_id { get; set; }

        [Display(Name = "Inicio")]
        public DateTime DataInicial { get; set; }

        [Display(Name = "Fim")]
        public DateTime DataFinal { get; set; }

        public string Observacao { get; set; }

        [ForeignKey("usuario_id")]
        public usuario Usuario { get; set; }

        [ForeignKey("motorista_id")]
        public motorista Motorista { get; set; }
    }
}
//dotnet aspnet-codegenerator controller -name Usuario_MotoristaController -m usuario_motorista -dc ApaDbContext --relativeFolderPath  Areas\Cadastro\Controllers\Usuarios --useDefaultLayout --referenceScriptLibraries
