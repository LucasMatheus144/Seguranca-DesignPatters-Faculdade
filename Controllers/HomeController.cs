using System.Text;
using EspacoPotencial.Areas.Cadastro.Models.Funcionarios;
using EspacoPotencial.Areas.Cadastro.Models.Public;
using EspacoPotencial.Areas.Cadastro.Models.Usuarios;
using EspacoPotencial.Context;
using EspacoPotencial.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EspacoPotencial.Models.Account;
using EspacoPotencial.Filters;
using EspacoPotencial.Models.Observer;


namespace EspacoPotencial.Controllers;

public class HomeController : Controller
{
    private readonly ApaDbContext _context;
    public HomeController(ApaDbContext context)
    {
        _context = context;
    }
    [Authorize]
    public IActionResult Index()
    {   
        if (HttpContext.Session.TryGetValue("ErroAutorizacao", out byte[] errorMessageBytes))
        {
            var errorMessage = Encoding.UTF8.GetString(errorMessageBytes);
            TempData["ErrorMessage"] = errorMessage;
            HttpContext.Session.Remove("ErroAutorizacao");
        }   

        var geralFuncionarioQuery = _context.geral
            .Join(_context.funcionario, g => g.Id, f => f.geral_id, (g, f) => new CreateViewModel.Exibir
            {
                GeralId = g.Id,
                Nome = g.Nome,
                Foto = f.Foto,
                Email = g.Email1,
                Tipo = g.Tipo
            }) ;

        var geralUsuarioQuery = _context.geral
            .Join(_context.usuario, g => g.Id, u => u.geral_id, (g, u) => new CreateViewModel.Exibir
            {
                GeralId = g.Id,
                Nome = g.Nome,
                Foto = u.Foto,
                Email = g.Email1,
                Tipo = g.Tipo
            });

        var result = geralFuncionarioQuery.Concat(geralUsuarioQuery)
            .ToList();


          var viewModel = new CreateViewModel
        {
            Resultados = result,

        };

        ViewData["banco_id"] = new SelectList(_context.banco, "Id", "Nome");
        ViewData["centrocusto_id"] = new SelectList(_context.centro_custo, "Id", "Nome");
        ViewData["beneficio_id"] = new SelectList(_context.beneficio, "Id", "Nome");
        ViewData["comorbidade_id"] = new SelectList(_context.comorbidade, "Id", "Descricao");
        ViewData["escolas_id"] = new SelectList(_context.escolas, "Id", "Nome");
        ViewData["desligamento_id"] = new SelectList(_context.desligamento_motivos_usuario, "Id", "Motivo");

        return View(viewModel);
    }


    

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser, TipoUsuario.Admin})]   
        public async Task<IActionResult> Cadastro(CreateViewModel viewModel)
        {
            bool isGeralValid = viewModel.GeralModel != null;
            bool isFuncionarioValid = viewModel.FuncionarioModel != null;
            bool isUsuarioValid = viewModel.UsuarioModel != null;

            if (isGeralValid && (isFuncionarioValid || isUsuarioValid))
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var geralBuilder = new geral.Builder()
                            .GetTipo(viewModel.GeralModel.Tipo)
                            .GetNome(viewModel.GeralModel.Nome)
                            .GetSituacao(viewModel.GeralModel.Situacao)
                            .GetDocto(viewModel.GeralModel.Docto)
                            .GetEndereco(viewModel.GeralModel.Endereco)
                            .GetNumero(viewModel.GeralModel.Numero)
                            .GetComplemento(viewModel.GeralModel.Complemento)
                            .GetCidade(viewModel.GeralModel.Cidade)
                            .GetEstado(viewModel.GeralModel.Estado)
                            .GetTelefone1(viewModel.GeralModel.Telefone1)
                            .GetTelefone2(viewModel.GeralModel.Telefone2)
                            .GetEmail1(viewModel.GeralModel.Email1)
                            .GetEmail2(viewModel.GeralModel.Email2)
                            .GetCep(viewModel.GeralModel.Cep)
                            .GetDataCadastro(DateTime.Now);
                        
                        var novoGeral = geralBuilder.Build();
                        _context.Add(novoGeral);
                        await _context.SaveChangesAsync();
                        int novoGeralId = novoGeral.Id;

                        ITipo funcionarioObserver = new FuncionarioObserver(viewModel, novoGeralId, new funcionario(), _context);
                        ITipo usuarioObserver = new UsuarioObserver(viewModel, novoGeralId, new usuario(), _context);

                        geralBuilder.RegisterObserver(funcionarioObserver);
                        geralBuilder.RegisterObserver(usuarioObserver);

                        transaction.Commit(); 
                        TempData["SuccessMessage"] = "Cadastro realizado com sucesso!";

                        return RedirectToAction(nameof(Index), "Home");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback(); 
                        string errorMessage = $"Ocorreu um erro ao processar o cadastro: {ex.Message}";

                        Exception innerException = ex.InnerException;
                        while (innerException != null)
                        {
                            errorMessage += $"\nInner Exception: {innerException.Message}";
                            innerException = innerException.InnerException;
                        }

                        TempData["ErrorMessage"] = errorMessage;
                        return RedirectToAction(nameof(Index)); // Redireciona em caso de falha
                    }
                }
            }

            return RedirectToAction(nameof(Index));
        }
}

