using EspacoPotencial.Areas.Cadastro.Models.Public;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoPotencial.Areas.Cadastro.Models.Funcionarios
{
    [Table("funcionario", Schema = "funcionario")]
    public class funcionario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Funcionario")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe nome do Cadastro geral")]
        [Display(Name = "Nome")]
        public int geral_id { get; set; }

        [Display(Name = "Foto")]
        public byte[] Foto { get; set; }

        [StringLength(12)]
        [Display(Name = "RG")]
        public string Rg { get; set; }

        [Display(Name = "Nascimento")]
        [Required(ErrorMessage = "Informe a data de nascimento")]
        [DataType(DataType.Date)]
        public DateTime Nascimento { get; set; }

        [Display(Name = "Centro Custo")]
        [Required(ErrorMessage = "Informe o centro custo")]
        public int centrocusto_id { get; set; }

        [StringLength(1)]
        [Display(Name = "Cesta Basica")]
        [Required(ErrorMessage = "Informe se deseja receber a cesta basica")]
        public string CestaBasica { get; set; }

        [Display(Name = "Banco")]
        [Required(ErrorMessage = "Informe o banco")]
        public int banco_id { get; set; }

        [StringLength(7)]
        [Display(Name = "Agencia")]
        [Required(ErrorMessage = "Informe a agencia do funcionario")]
        public string Agencia { get; set; }

        [StringLength(11)]
        [Display(Name = "Conta")]
        [Required(ErrorMessage = "Informe a conta do funcionario")]
        public string Conta { get; set; }

        [StringLength(3)]
        [Display(Name = "Escolaridade")]
        public string Escolaridade { get; set; }

        //[RequiredIf("funcionario_escolaridade", ErrorMessage = "A formação é obrigatória quando a escolaridade é preenchida.")]
        [Display(Name = "Data Formação")]
        [Required(ErrorMessage = "Informe a data de formação")]
        [DataType(DataType.Date)]
        public DateTime Formacao { get; set; }

        [ForeignKey("geral_id")]
        public geral geral { get; set; }

        [ForeignKey("centrocusto_id")]
        public centro_custo centro_custo { get; set; }

        [ForeignKey("banco_id")]
        public banco banco { get; set; }

        [NotMapped]
        [DataType(DataType.Upload)]
        [ValidateFile(ErrorMessage = "Somente arquivos JPG e PNG são permitidos.")]
        [Display(Name ="Foto")]
        public IFormFile ImagemFile { get; set; }
    }


}


//dotnet aspnet-codegenerator controller -name FuncionarioController -m funcionario -dc ApaDbContext --relativeFolderPath Areas\Cadastro\Controllers --useDefaultLayout --referenceScriptLibraries
