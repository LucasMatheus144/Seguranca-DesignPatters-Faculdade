using EspacoPotencial.Areas.Cadastro.Models.Funcionarios;
using EspacoPotencial.Areas.Cadastro.Models.Public;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoPotencial.Areas.Cadastro.Models.Usuarios
{
    [Table("usuario", Schema = "usuarios")]
    public class usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int geral_id { get; set; }

        [MaxLength(1)]
        public string Situacao { get; set; }

        [Display(Name = "Foto")]
        public byte[] Foto { get; set; }

        [NotMapped]
        public string caminhofoto { get; set; }

        [MaxLength(30)]
        [Required(ErrorMessage = "Informe o nome")]
        [Display(Name = "Numero SUS")]
        public string Sus { get; set; }

        [Display(Name = "Nascimento")]
        [Required(ErrorMessage = "Informe a data de nascimento")]
        [DataType(DataType.Date)]
        public DateTime Nascimento { get; set; }

        [Column("usuario_ingresso")]
        [Display(Name = "Data de Inicio")]
        [DataType(DataType.Date)]
        public DateTime Ingresso { get; set; }

        [Display(Name = "Data Laudo")]
        [DataType(DataType.Date)]
        public DateTime DataLaudo { get; set; }

        [Display(Name = "Escola")]
        public int escolas_id { get; set; }

        [MaxLength(2)]
        [Display(Name = "Serie")]
        public string Serie { get; set; }

        [Display(Name = "Beneficio")]
        public int beneficio_id { get; set; }

        [Display(Name = "Comorbidade")]
        public int comorbidade_id { get; set; }

        [Display(Name = "Historico")]
        public string HistoricoContato { get; set; }

        [Display(Name = "Desligamento")]
        public int? desligamento_id { get; set; }

        [Display(Name = "Data Desligamento")]
        [DataType(DataType.Date)]
        public DateTime DataDesligamento { get; set; }

        [Display(Name = "Descrição do motivo")]
        public string usuario_descritivo_desligamento { get; set; }

        [Display(Name = "Alergia")]
        public string Alergia { get; set; }

        [Column("usuario_medicacao")]
        [Display(Name = "Medicação")]
        public string Medicacao { get; set; }

        [Display(Name = "Restrição Alimentar")]
        public string RestricaoAlimentar { get; set; }

        [MaxLength(1)]
        [Display(Name = "Transporte")]
        public string Transporte { get; set; }
        
        [NotMapped]
        [DataType(DataType.Upload)]
        [ValidateFile]
        [Display(Name ="Foto")]
        public IFormFile ImagemFile { get; set; }

        [ForeignKey("escolas_id")]
        public escolas Escola { get; set; }

        [ForeignKey("beneficio_id")]
        public beneficio Beneficio { get; set; }

        [ForeignKey("comorbidade_id")]
        public comorbidade Comorbidade { get; set; }

        [ForeignKey("desligamento_id")]
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public desligamento_motivos_usuario? MotivoDesligamento { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

        [ForeignKey("geral_id")]
        public geral Geral {get; set;}
    }
}
//dotnet aspnet-codegenerator controller -name UsuarioController -m usuario -dc ApaDbContext --relativeFolderPath  Areas\Cadastro\Controllers\Usuarios --useDefaultLayout --referenceScriptLibraries
