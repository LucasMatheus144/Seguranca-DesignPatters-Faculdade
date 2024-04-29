using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EspacoPotencial.Areas.Cadastro.Models.Funcionarios;
using EspacoPotencial.Context;
using EspacoPotencial.Filters;
using EspacoPotencial.Models.Account;

namespace EspacoPotencial.Areas.Cadastro.Controllers.Funcionario
{
    [Area("Cadastro")]
    [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
    public class FuncionarioController : Controller
    {
        private readonly ApaDbContext _context;

        public FuncionarioController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/Funcionario
        public async Task<IActionResult> Index()
        {
            var apaDbContext = _context.funcionario.Include(f => f.banco).Include(f => f.centro_custo).Include(f => f.geral);
            return View(await apaDbContext.ToListAsync());
        }

        // GET: Cadastro/Funcionario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.funcionario
                .Include(f => f.banco)
                .Include(f => f.centro_custo)
                .Include(f => f.geral)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // GET: Cadastro/Funcionario/Create
        public IActionResult Create()
        {
            ViewData["banco_id"] = new SelectList(_context.banco, "Id", "Nome");
            ViewData["centrocusto_id"] = new SelectList(_context.centro_custo, "Id", "Nome");
            ViewData["geral_id"] = new SelectList(_context.geral, "Id", "Cep");
            return View();
        }

        // POST: Cadastro/Funcionario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,geral_id,Foto,Rg,Nascimento,centrocusto_id,CestaBasica,banco_id,Agencia,Conta,Escolaridade,Formacao")] funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(funcionario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["banco_id"] = new SelectList(_context.banco, "Id", "Nome", funcionario.banco_id);
            ViewData["centrocusto_id"] = new SelectList(_context.centro_custo, "Id", "Nome", funcionario.centrocusto_id);
            ViewData["geral_id"] = new SelectList(_context.geral, "Id", "Cep", funcionario.geral_id);
            return View(funcionario);
        }

        // GET: Cadastro/Funcionario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.funcionario.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }
            ViewData["banco_id"] = new SelectList(_context.banco, "Id", "Nome", funcionario.banco_id);
            ViewData["centrocusto_id"] = new SelectList(_context.centro_custo, "Id", "Nome", funcionario.centrocusto_id);
            ViewData["geral_id"] = new SelectList(_context.geral, "Id", "Cep", funcionario.geral_id);
            return View(funcionario);
        }

        // POST: Cadastro/Funcionario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,geral_id,Foto,Rg,Nascimento,centrocusto_id,CestaBasica,banco_id,Agencia,Conta,Escolaridade,Formacao")] funcionario funcionario)
        {
            if (id != funcionario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcionario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!funcionarioExists(funcionario.Id))
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
            ViewData["banco_id"] = new SelectList(_context.banco, "Id", "Nome", funcionario.banco_id);
            ViewData["centrocusto_id"] = new SelectList(_context.centro_custo, "Id", "Nome", funcionario.centrocusto_id);
            ViewData["geral_id"] = new SelectList(_context.geral, "Id", "Cep", funcionario.geral_id);
            return View(funcionario);
        }

        // GET: Cadastro/Funcionario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.funcionario
                .Include(f => f.banco)
                .Include(f => f.centro_custo)
                .Include(f => f.geral)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // POST: Cadastro/Funcionario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcionario = await _context.funcionario.FindAsync(id);
            _context.funcionario.Remove(funcionario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool funcionarioExists(int id)
        {
            return _context.funcionario.Any(e => e.Id == id);
        }
    }
}
