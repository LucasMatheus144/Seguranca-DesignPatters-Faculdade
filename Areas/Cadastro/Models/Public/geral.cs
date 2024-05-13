using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EspacoPotencial.Models.Observer;

namespace EspacoPotencial.Areas.Cadastro.Models.Public
{
    public class geral
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /* [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Configuração para autoincremento
        public int Id { get; set; }
*/

        [Required]
        [StringLength(1)]
        [Display(Name = "Tipo")]
        public string Tipo { get; set; } //'1-Funcionario|2-Usuario|3-Responsavel|4-Terceiro';

        [StringLength(50)]
        [Required(ErrorMessage = "Informe o nome")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Situaçao do Acesso")]
        [StringLength(1)]
        [Display(Name = "Situação")]
        public string Situacao { get; set; } // '1-Ativo|2-Inativo';

        [StringLength(50)]
        [Required(ErrorMessage = "Nome Docto")]
        [Display(Name = "Doctor")]
        public string Docto { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Endereço")]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [StringLength(6)]
        [Required(ErrorMessage = "Numero Do Endereço")]
        [Display(Name = "Numero")]
        public string Numero { get; set; }

        [StringLength(30)]
        [Display(Name = "Complemento")]
        public string Complemento { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Cidade")]
        [Display(Name = "Cidade")]
        public string Cidade { get; set; }

        [StringLength(2)]
        [Required(ErrorMessage = "Estado")]
        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [StringLength(9)]
        [Required(ErrorMessage = "Cep")]
        [Display(Name = "Cep")]
        public string Cep { get; set; }

        public DateTime DataCadastro { get; set; }

        [Required(ErrorMessage = "Telefone")]
        [Display(Name = "Telefone")]
        [StringLength(14)]
        public string Telefone1 { get; set; }

        [StringLength(14)]
        [Display(Name = "Telefone Extra")]
        public string Telefone2 { get; set; }

        [StringLength(40)]
        [Required(ErrorMessage = "Email")]
        [Display(Name = "Email")]
        public string Email1 { get; set; }

        [StringLength(40)]
        [Display(Name = "Email Extra")]
        public string Email2 { get; set; }

        public class Builder
        {
            private geral _geral;
            private ITipo _observer;

            public Builder()
            {
                _geral = new geral();
            }

            public void RegisterObserver(ITipo observer)
            {
                _observer = observer;
            }

            private void NotifyObservers(string tipo)
            {
                _observer?.AtualizarTipo(tipo);
            }

            public Builder GetTipo(string tipo)
            {
                _geral.Tipo = tipo;
                NotifyObservers(tipo);
                return this;
            }


            public Builder GetNome(string nome)
            {
                _geral.Nome = nome;
                return this;
            }

            public Builder GetSituacao(string situacao)
            {
                _geral.Situacao = situacao;
                return this;
            }

            public Builder GetDocto(string docto)
            {
                _geral.Docto = docto;
                return this;
            }

            public Builder GetEndereco(string endereco)
            {
                _geral.Endereco = endereco;
                return this;
            }

            public Builder GetNumero(string numero)
            {
                _geral.Numero = numero;
                return this;
            }

            public Builder GetComplemento(string complemento)
            {
                _geral.Complemento = complemento;
                return this;
            }

            public Builder GetCidade(string cidade)
            {
                _geral.Cidade = cidade;
                return this;
            }

            public Builder GetEstado(string estado)
            {
                _geral.Estado = estado;
                return this;
            }

            public Builder GetCep(string cep)
            {
                _geral.Cep = cep;
                return this;
            }

            public Builder GetDataCadastro(DateTime dataCadastro)
            {
                _geral.DataCadastro = dataCadastro;
                return this;
            }

            public Builder GetTelefone1(string telefone1)
            {
                _geral.Telefone1 = telefone1;
                return this;
            }

            public Builder GetTelefone2(string telefone2)
            {
                _geral.Telefone2 = telefone2;
                return this;
            }

            public Builder GetEmail1(string email1)
            {
                _geral.Email1 = email1;
                return this;
            }

            public Builder GetEmail2(string email2)
            {
                _geral.Email2 = email2;
                return this;
            }

            public geral Build()
            {
                return _geral;
            }
        }
    }
}

//dotnet aspnet-codegenerator controller -name GeralController -m geral -dc ApaDbContext --relativeFolderPath Areas\Cadastro\Controllers --useDefaultLayout --referenceScriptLibraries
