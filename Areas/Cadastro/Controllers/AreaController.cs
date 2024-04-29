using EspacoPotencial.Filters;
using EspacoPotencial.Models.Account;
using Microsoft.AspNetCore.Mvc;

namespace EspacoPotencial.Areas.Cadastro.Controllers
{
    [Area("Cadastro")]
    public class AreaController : Controller
    {
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin , TipoUsuario.Funcionarios})]
        public IActionResult Menu()
        {
            return View();
        }
    }
}
