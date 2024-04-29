using System.ComponentModel.DataAnnotations;

namespace EspacoPotencial.Models.Account
{
    public class Login
    {
        [Key] public int id_Login { get; set; }
        [Required(ErrorMessage = "Informe o nome")]
        [Display(Name = "Usuario")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

    }
}

