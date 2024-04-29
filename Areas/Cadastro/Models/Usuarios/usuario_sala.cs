using EspacoPotencial.Areas.Cadastro.Models.Public;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EspacoPotencial.Areas.Cadastro.Models.Usuarios
{
    [Table("usuario_sala", Schema = "usuarios")]
    public class usuario_sala
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int geral_id { get; set; }

        [Display(Name = "Nome do Usuario")]
        [Required(ErrorMessage = "Informe o nome do usuario")]
        public int usuario_id { get; set; }

        [Display(Name = "Sala")]
        [Required(ErrorMessage = "Informe a sala")]
        public int sala_id { get; set; }

        [MaxLength(1)]
        [Display(Name = "Periodo")]
        [Required(ErrorMessage = "Informe a data de formação")]
        public string Periodo { get; set; }

        [Display(Name = "Data Final")]
        [Required(ErrorMessage = "Informe a data de formação")]
        [DataType(DataType.Date)]
        public DateTime Inicio { get; set; }

        [Display(Name = "Data Final")]
        [Required(ErrorMessage = "Informe a data de formação")]
        [DataType(DataType.Date)]
        public DateTime Final { get; set; }

        [ForeignKey("usuario_id")]
        public usuario Usuario { get; set; }

        [ForeignKey("geral_id")]
        public geral Geral { get; set; }

        [ForeignKey("sala_id")]
        public sala Sala { get; set; }
    }
}
//dotnet aspnet-codegenerator controller -name UsuarioSalaController -m usuario_sala -dc ApaDbContext --relativeFolderPath  Areas\Cadastro\Controllers\Usuarios --useDefaultLayout --referenceScriptLibraries
