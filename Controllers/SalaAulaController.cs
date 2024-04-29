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
using static EspacoPotencial.Models.ChamadaViewModel;

namespace EspacoPotencial.Controllers;

[Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
public class SalaAulaController : Controller{

      private readonly ApaDbContext _context;
       public SalaAulaController(ApaDbContext context)
    {
        _context = context;
    }
    public IActionResult Aula(string search = "roxo")
    {
        var responsavelQuery = from ggg in _context.geral
                            join res in _context.responsavel on ggg.Id equals res.geral_func
                            select new 
                            {
                                res.usuario_id,
                                NomeResponsavel = ggg.Nome,
                                TelefoneResponsavel = ggg.Telefone1
                            };

        var query = from g in _context.geral
                    join u in _context.usuario on g.Id equals u.geral_id
                    join us in _context.usuario_sala on u.Id equals us.usuario_id
                    join s in _context.sala on us.sala_id equals s.Id
                    join es in _context.educador_sala on s.Id equals es.sala_id into esGroup
                    from es in esGroup.DefaultIfEmpty()
                    join f in _context.funcionario on es.funcionario_id equals f.Id into fGroup
                    from f in fGroup.DefaultIfEmpty()
                    join g1 in _context.geral on f.geral_id equals g1.Id into g1Group
                    from g1 in g1Group.DefaultIfEmpty()
                    join res in _context.responsavel on g.Id equals res.geral_id into resGroup
                    from res in resGroup.DefaultIfEmpty()
                    join gf1 in _context.geral on res.geral_id equals gf1.Id into gf1Group
                    from gf1 in gf1Group.DefaultIfEmpty()
                    join resp in responsavelQuery on u.Id equals resp.usuario_id into respGroup
                    from resp in respGroup.DefaultIfEmpty()
                    where EF.Functions.ILike(s.Nome, $"%{search}%")
                    select new 
                    { 
                        NomeSala = s.Nome, 
                        NomeUsuario = g.Nome, 
                        NomeResponsavel = resp.NomeResponsavel,
                        TelefoneResponsavel = resp.TelefoneResponsavel,
                        NomeProfessor = g1.Nome,
                        IdSala = s.Id,
                        IdUsuario = u.Id,
                        CodPeriodo = s.Periodo
                    };

       var groupedQuery = query.ToList() 
        .GroupBy(item => new { item.NomeSala, item.NomeUsuario, item.NomeResponsavel, item.TelefoneResponsavel, item.IdSala, item.CodPeriodo, item.IdUsuario })
        .Select(group => new SalaAulaModel.Exibir
        {
            NomeSala = group.Key.NomeSala,
            NomeUsario = group.Key.NomeUsuario,
            NomeResponsavel = group.Key.NomeResponsavel,
            TelefoneResponsavel = group.Key.TelefoneResponsavel,
            IdSala = group.Key.IdSala,
            IdUsuario = group.Key.IdUsuario,
            CodPeriodo = group.Key.CodPeriodo, 
            NomeProfessor = string.Join(", ", group.Select(g => g.NomeProfessor))
        });

        var model = new SalaAulaModel
        {
            Resultados = groupedQuery.ToList()
        };

        return View(model);
    }



    public IActionResult Chamada(int IdSala)
    {
        var usuariosSala = _context.usuario_sala.Include(u => u.Usuario)
                                    .Where(u => u.sala_id == IdSala)
                                    .Select(u => new ChamadaViewModel.Mostrar
                                    {  
                                        Id = u.usuario_id,
                                        NomeSala = u.Sala.Nome,
                                        NomeUsario = u.Geral.Nome,
                                        IdSala = u.sala_id,
                                        UsuarioId = u.Usuario.Id

                                    })
                                    .ToList();

        var model = new ChamadaViewModel
        {
            Resultados = usuariosSala
        };

       ViewData["ChamadaData"] = model;
       

    
       return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Chamada(List<ChamadaViewModel.Mostrar> chamadas, int IdSala)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var frequencia = new List<frequencia>();

                foreach (var mostrar in chamadas)
                {
                    var chamadaData = new frequencia
                    {
                        sala_id = IdSala,
                        usuario_id = mostrar.UsuarioId, // Definindo o usuario_id com o valor de UsuarioId do item
                        Observacao = mostrar.Observacao,
                        Data = DateTime.Today, // Definir como a data e hora atuais
                        Registro = mostrar.Registro
                    };

                    frequencia.Add(chamadaData);
                }
                _context.AddRange(frequencia);
                await _context.SaveChangesAsync();
                return RedirectToAction("Aula");
            }
            catch (Exception)
            {
                // Lidar com a exceção aqui
                // Você pode fazer log do erro ou retornar uma mensagem de erro para a view, por exemplo
                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar os dados.");
                return View("Aula");
            }
        }

        // Se o modelo não for válido, você pode retornar a view com os erros de validação
        return View("Aula");
    }




    




}