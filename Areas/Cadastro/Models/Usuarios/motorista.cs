using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoPotencial.Areas.Cadastro.Models.Usuarios
{
    [Table("motorista", Schema = "usuarios")]
    public class motorista
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Nome { get; set; }

        [MaxLength(1)]
        public string Situacao { get; set; }

        [MaxLength(14)] 
        public string Cpf { get; set; }

        [MaxLength(12)]
        public string Rg { get; set; }

        public string Observacao { get; set; }

        [MaxLength(8)]
        public string PlacaVeiculo { get; set; }

        [MaxLength(2)]
        public string CapacidadeVeiculo { get; set; }
    }
}

//dotnet aspnet-codegenerator controller -name MotoristaController -m motorista -dc ApaDbContext --relativeFolderPath  Areas\Cadastro\Controllers\Usuarios --useDefaultLayout --referenceScriptLibraries
