using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoPotencial.Areas.Cadastro.Models.Usuarios
{
    [Table("datas", Schema = "usuarios")]
    public class datas
    {
        [Key]
        [Column("datas_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(4)]
        public string Ano { get; set; }

        [MaxLength(1)]
        public string Tipo { get; set; }

        public string Descritivo { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFinal { get; set; }
    }
}
//dotnet aspnet-codegenerator controller -name DatasController -m datas -dc ApaDbContext --relativeFolderPath  Areas\Cadastro\Controllers\Usuarios --useDefaultLayout --referenceScriptLibraries
