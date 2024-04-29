using EspacoPotencial.Context;
using EspacoPotencial.Filters;
using EspacoPotencial.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;




namespace Espacopotencial.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly ApaDbContext _context;




        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ApaDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
            ;

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return View(new Login()
            {
                ReturnUrl = returnUrl
            });
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(Login loginVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByNameAsync(loginVM.Username);
            if (user == null)
            {
                ModelState.AddModelError("", "Falha ao realizar o login. Verifique suas credenciais.");
                return View(loginVM);
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, true);

            if (result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);
                return RedirectToAction("Index", "Home", new { roles = string.Join(",", roles) });
            }

             if (result.IsLockedOut){
                TempData["ErrorMessage"] ="Essa Conta foi bloqueada por 2 minutos por tentativa excedida";
               
            }

            ModelState.AddModelError("", "Falha ao realizar o login. Verifique suas credenciais.");
            return View(loginVM);
        }


       [Autorizacao(new[] { TipoUsuario.SuperUser })]
        public IActionResult Acesso()
        {
            return View();
        }


        // Criar Usuario
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser })]       
        public async Task<IActionResult> Acesso(Acesso registroVM)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByNameAsync(registroVM.Login);
                if (existingUser != null)
                {
                    ModelState.AddModelError(nameof(registroVM.Login), "Esse Login está inválido. Por favor, utilize outro!");
                    return View(registroVM);
                }

                var user = new IdentityUser
                {
                    UserName = registroVM.Login,
                    Email = registroVM.Email,

                };

                var result = await _userManager.CreateAsync(user, registroVM.Senha);

                if (result.Succeeded)
                {
                    var roleExists = await _roleManager.RoleExistsAsync(registroVM.Permissao);

                    if (!roleExists)
                    {
                        await _roleManager.CreateAsync(new IdentityRole(registroVM.Permissao));
                    }

                    await _userManager.AddToRoleAsync(user, registroVM.Permissao);

                    await _userManager.AddClaimAsync(user, new Claim("Permissao", registroVM.Permissao));
                    await _userManager.AddClaimAsync(user, new Claim("Nome", registroVM.Nome));
                    await _userManager.AddClaimAsync(user, new Claim("Email", registroVM.Email));

                    return RedirectToAction("ListagemAcesso", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(registroVM);
        }



        [Authorize]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.User = null;
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }


        [Autorizacao(new[] { TipoUsuario.SuperUser })]
        public IActionResult ListagemAcesso()
        {
            var usuariosComRoles = _context.Users
                .Join(_context.UserRoles, u => u.Id, ur => ur.UserId, (u, ur) => new { User = u, UserRole = ur })
                .Join(_context.Roles, ur => ur.UserRole.RoleId, r => r.Id, (ur, r) => new Acesso
                {
                    Id = ur.User.Id,
                    Nome = ur.User.UserName, 
                    Email = ur.User.Email,
                    Permissao = r.Name 
                })
                .ToList();

            return View(usuariosComRoles);
        }


        [HttpPost]
        [Autorizacao(new[] { TipoUsuario.SuperUser })]
        public async Task<IActionResult> AtualizarUsuario(string id, string roleName)
        {
            var usuario = await _userManager.FindByIdAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(usuario);
            await _userManager.RemoveFromRolesAsync(usuario, userRoles);

            var result = await _userManager.AddToRoleAsync(usuario, roleName);

            if (result.Succeeded)
            {
                return RedirectToAction("ListagemAcesso", "Account");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View("EditarUsuario", usuario); 
            }
        }


        //Excluir

        [Autorizacao(new[] { TipoUsuario.SuperUser })]
        public async Task<IActionResult> ExclusaoAcesso(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);

            return View(usuario);
        }

        [HttpPost]
        [Autorizacao(new[] { TipoUsuario.SuperUser })]
        public async Task<IActionResult> ConfirmarExclusao(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(usuario);

            if (result.Succeeded)
            {
                return RedirectToAction("ListagemAcesso", "Account");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    TempData["ErrorMessage"] = error.Description;
                }
                return RedirectToAction("ListagemAcesso", "Account");
            }
        }

        //Editar
        [Autorizacao(new[] { TipoUsuario.SuperUser })]
        public async Task<IActionResult> EdicaoAcesso(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);
            return View(usuario);
        }




    }

    

    
}

