using EspacoPotencial.Models.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EspacoPotencial.Filters
{
    public class Autorizacao : Attribute, IAuthorizationFilter
    {
        private TipoUsuario[] TipoAutorizados;

        public Autorizacao(TipoUsuario[] tiposUsuariosAutorizados)
        {
            TipoAutorizados = tiposUsuariosAutorizados;
        }

       public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (!IsAutorizado(filterContext))
            {
                SetErroAutorizacao(filterContext, filterContext.HttpContext.Session);
            }
        }

        private bool IsAutorizado(AuthorizationFilterContext filterContext)
        {
            return TipoAutorizados.Any(t => filterContext.HttpContext.User.IsInRole(t.ToString()));
        }
        private void SetErroAutorizacao(AuthorizationFilterContext filterContext, ISession session)
        {
            var errorMessage = "Você não tem permissão para acessar esta página.";
            session.SetString("ErroAutorizacao", errorMessage);

            filterContext.Result = new RedirectToActionResult("Index", "Home", new { area = "" });
        }
    }

}