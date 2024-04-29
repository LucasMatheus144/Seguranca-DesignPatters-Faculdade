using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EspacoPotencial.Areas.Cadastro.Models.Public;
using EspacoPotencial.Context;
using EspacoPotencial.Filters;
using EspacoPotencial.Models.Account;

namespace EspacoPotencial.Areas.Cadastro.Controllers.Public
{
    [Area("Cadastro")]
    [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin , TipoUsuario.Funcionarios})]
    public class GeralController : Controller
    {
        private readonly ApaDbContext _context;

        public GeralController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/Geral
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin , TipoUsuario.Funcionarios})]
        public async Task<IActionResult> Index()
        {
            return View(await _context.geral.ToListAsync());
        }

        // GET: Cadastro/Geral/Details/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin , TipoUsuario.Funcionarios})]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var geral = await _context.geral
                .FirstOrDefaultAsync(m => m.Id == id);
            if (geral == null)
            {
                return NotFound();
            }

            return View(geral);
        }

        // GET: Cadastro/Geral/Create
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin , TipoUsuario.Funcionarios})]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cadastro/Geral/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tipo,Nome,Situacao,Docto,Endereco,Numero,Complemento,Cidade,Estado,Cep,DataCadastro,Telefone1,Telefone2,Email1,Email2")] geral geral)
        {
            if (ModelState.IsValid)
            {
                geral.DataCadastro = DateTime.Now;
                _context.Add(geral);
                await _context.SaveChangesAsync();

                if (geral.Tipo == "2")
                {
                    await Task.Delay(1000); //delay de 1 segundos
                    return RedirectToAction(nameof(Index)); // faZER O DO USUARIO
                }
                else
                {
                    await Task.Delay(1000);
                    return RedirectToAction("Create", "Funcionario", new { area = "Cadastro" });
                }
            }
            return View(geral);
        }

        // GET: Cadastro/Geral/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var geral = await _context.geral.FindAsync(id);
            if (geral == null)
            {
                return NotFound();
            }
            return View(geral);
        }

        // POST: Cadastro/Geral/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tipo,Nome,Situacao,Docto,Endereco,Numero,Complemento,Cidade,Estado,Cep,DataCadastro,Telefone1,Telefone2,Email1,Email2")] geral geral)
        {
            if (id != geral.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(geral);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!geralExists(geral.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(geral);
        }

        // GET: Cadastro/Geral/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var geral = await _context.geral
                .FirstOrDefaultAsync(m => m.Id == id);
            if (geral == null)
            {
                return NotFound();
            }

            return View(geral);
        }

        // POST: Cadastro/Geral/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var geral = await _context.geral.FindAsync(id);
            _context.geral.Remove(geral);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool geralExists(int id)
        {
            return _context.geral.Any(e => e.Id == id);
        }
    }
}
