using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoPotencial.Areas.Cadastro.Models.Usuarios
{
    [Table("escolas", Schema = "usuarios")]
    public class escolas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Informe o nome")]
        public string Nome { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Informe a cidade")]
        public string Cidade { get; set; }

        [MaxLength(14)]
        [Required(ErrorMessage = "Informe o telefone")]
        public string Telefone { get; set; }
    }
}

//dotnet aspnet-codegenerator controller -name EscolaController -m escolas -dc ApaDbContext --relativeFolderPath  Areas\Cadastro\Controllers\Usuarios --useDefaultLayout --referenceScriptLibraries

