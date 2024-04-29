using EspacoPotencial.Areas.Cadastro.Models.Funcionarios;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EspacoPotencial.Areas.Cadastro.Models.Usuarios
{
    [Table("educador_sala", Schema = "usuarios")]
    public class educador_sala
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o Nome")]
        [Display(Name = "Nome")]
        public int funcionario_id { get; set; }

        [Display(Name = "Sala")]
        public int sala_id {get; set; }

        [Required(ErrorMessage = "Informe o inicio")]
        [DataType(DataType.Date)]
        public DateTime Inicio { get; set; }

        [Required(ErrorMessage = "Informe o final")]
        [DataType(DataType.Date)]
        public DateTime Final { get; set; }

        [ForeignKey("funcionario_id")]
        public funcionario Funcionario { get; set; }

        [ForeignKey("sala_id")]
        public sala Sala { get; set; }
    }
}
//dotnet aspnet-codegenerator controller -name EducadorSalaController -m educador_sala -dc ApaDbContext --relativeFolderPath  Areas\Cadastro\Controllers\Usuarios --useDefaultLayout --referenceScriptLibraries
