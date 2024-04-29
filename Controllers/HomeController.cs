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
    [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]   
    public async Task<IActionResult> Cadastro(CreateViewModel viewModel)
    {

        bool isGeralValid = viewModel.GeralModel != null;
        bool isFuncionarioValid = viewModel.FuncionarioModel != null;
        bool isUsuarioValid  = viewModel.UsuarioModel  != null;

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

                   if (!(viewModel.GeralModel.Tipo == "2")){

                        var novoFuncionario = new funcionario
                        {
                            geral_id = novoGeralId,
                            Rg = viewModel.FuncionarioModel.Rg,
                            Nascimento = viewModel.FuncionarioModel.Nascimento,
                            centrocusto_id = viewModel.FuncionarioModel.centrocusto_id,
                            CestaBasica = viewModel.FuncionarioModel.CestaBasica,
                            banco_id = viewModel.FuncionarioModel.banco_id,
                            Agencia = viewModel.FuncionarioModel.Agencia,
                            Conta = viewModel.FuncionarioModel.Conta,
                            Escolaridade = viewModel.FuncionarioModel.Escolaridade,
                            Formacao = viewModel.FuncionarioModel.Formacao
                        };

                    if (viewModel.FuncionarioModel.ImagemFile != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await viewModel.FuncionarioModel.ImagemFile.CopyToAsync(memoryStream);
                            novoFuncionario.Foto = memoryStream.ToArray();
                        }
                    }


                        _context.Add(novoFuncionario);

                        await _context.SaveChangesAsync();
                        transaction.Commit(); 
                        TempData["SuccessMessage"] = "Cadastro realizado com sucesso!";

                    }else{
                        
                        var usuario = new usuario{

                            geral_id = novoGeralId,
                            Situacao = viewModel.UsuarioModel.Situacao,
                            Foto = viewModel.UsuarioModel.Foto,
                            Sus = viewModel.UsuarioModel.Sus,
                            Nascimento = viewModel.UsuarioModel.Nascimento,
                            Ingresso = viewModel.UsuarioModel.Ingresso,
                            DataLaudo = viewModel.UsuarioModel.DataLaudo,
                            escolas_id = viewModel.UsuarioModel.escolas_id,
                            comorbidade_id = viewModel.UsuarioModel.comorbidade_id,
                            beneficio_id = viewModel.UsuarioModel.beneficio_id,
                            HistoricoContato = viewModel.UsuarioModel.HistoricoContato,
                            desligamento_id = viewModel.UsuarioModel.desligamento_id,
                            usuario_descritivo_desligamento = viewModel.UsuarioModel.usuario_descritivo_desligamento,
                            Alergia = viewModel.UsuarioModel.Alergia,
                            Medicacao = viewModel.UsuarioModel.Medicacao,
                            RestricaoAlimentar = viewModel.UsuarioModel.RestricaoAlimentar,
                            Transporte = viewModel.UsuarioModel.Transporte
                        };

                                      
                        if (viewModel.UsuarioModel.ImagemFile != null)
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                await viewModel.UsuarioModel.ImagemFile.CopyToAsync(memoryStream);
                                usuario.Foto = memoryStream.ToArray();
                            }
                        } 

                        _context.Add(usuario);

                        await _context.SaveChangesAsync();
                        transaction.Commit(); 
                        TempData["SuccessMessage"] = "Cadastro realizado com sucesso!";
                    }

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