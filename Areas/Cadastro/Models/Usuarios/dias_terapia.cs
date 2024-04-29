using EspacoPotencial.Areas.Cadastro.Models.Public;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoPotencial.Areas.Cadastro.Models.Usuarios
{
    [Table("dias_terapia", Schema = "usuarios")]
    public class dias_terapia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o usuario")]
        [Display(Name = "Nome")]
        public int geral_id { get; set; }

        [Column("terapia_data_inicial")]
        [Display(Name = "Data do Inicio")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime DataInicial { get; set; }

        [Display(Name = "Data Final")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime DataFinal { get; set; }

        public bool segunda { get; set; }
        public bool terca { get; set; }
        public bool quarta { get; set; }
        public bool quinta { get; set; }
        public bool sexta { get; set; }
        
        
        public int DiaSemana
            {
                get
                {
                    return (segunda ? 1 : 0) + (terca ? 4 : 0) + (quarta ? 8 : 0) + (quinta ? 16 : 0) + (sexta ? 32 : 0);
                }
            }

        [ForeignKey("geral_id")]
        public geral Geral { get; set; }

        
    }
}
//dotnet aspnet-codegenerator controller -name DiasTerapiaController -m dias_terapia -dc ApaDbContext --relativeFolderPath  Areas\Cadastro\Controllers\Usuarios --useDefaultLayout --referenceScriptLibraries
