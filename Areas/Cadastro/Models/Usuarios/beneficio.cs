using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoPotencial.Areas.Cadastro.Models.Usuarios
{
    [Table("beneficio", Schema = "usuarios")]
    public class beneficio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Informe o nome do beneficio")]
        public string Nome { get; set; }
    }
}


//dotnet aspnet-codegenerator controller -name BeneficioController -m beneficio -dc ApaDbContext --relativeFolderPath Areas\Cadastro\Controllers\Usuarios --useDefaultLayout --referenceScriptLibraries
