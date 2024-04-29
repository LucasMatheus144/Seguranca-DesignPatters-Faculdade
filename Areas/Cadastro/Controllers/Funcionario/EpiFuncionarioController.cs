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
    public class EpiFuncionarioController : Controller
    {
        private readonly ApaDbContext _context;

        public EpiFuncionarioController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/EpiFuncionario
        public async Task<IActionResult> Index()
        {
            var apaDbContext = _context.epi_funcionario.Include(e => e.Funcionario).ThenInclude(f => f.geral).Include(e => e.epi_geral);
            return View(await apaDbContext.ToListAsync());
        }

        // GET: Cadastro/EpiFuncionario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var epi_funcionario = await _context.epi_funcionario
                .Include(e => e.Funcionario)
                .Include(e => e.epi_geral)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (epi_funcionario == null)
            {
                return NotFound();
            }

            return View(epi_funcionario);
        }

        // GET: Cadastro/EpiFuncionario/Create
        public IActionResult Create()
        {
            var funcionarios = _context.funcionario.Where(f => f.geral.Tipo == "1")
            .Select(f => new
            {
                Id = f.Id,
                Nome = f.geral.Nome
            }).ToList();
            ViewData["funcionario_id"] = new SelectList(funcionarios, "Id", "Nome");
            DateTime dataAtual = DateTime.Today;
            var epiValidas = _context.epi.Where(epi => epi.Validade >= dataAtual)
                                        .Select(epi => new { epi.Id, epi.Nome })
                                        .ToList();
            if (epiValidas.Any())
            {
                ViewData["epi_geral_id"] = new SelectList(epiValidas, "Id", "Nome");
            }

            return View();
        }

        // POST: Cadastro/EpiFuncionario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,funcionario_id,epi_geral_id,Vencimento,Entrega")] epi_funcionario epi_funcionario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(epi_funcionario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
             var funcionarios = _context.funcionario.Where(f => f.geral.Tipo == "1")
            .Select(f => new
            {
                Id = f.Id,
                Nome = f.geral.Nome
            }).ToList();
            ViewData["funcionario_id"] = new SelectList(funcionarios, "Id", "Nome");
            ViewData["epi_geral_id"] = new SelectList(_context.epi, "Id", "Nome", epi_funcionario.epi_geral_id);
            return View(epi_funcionario);
        }

        // GET: Cadastro/EpiFuncionario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var epi_funcionario = await _context.epi_funcionario.FindAsync(id);
            if (epi_funcionario == null)
            {
                return NotFound();
            }
             var funcionarios = _context.funcionario.Where(f => f.geral.Tipo == "1")
            .Select(f => new
            {
                Id = f.Id,
                Nome = f.geral.Nome
            }).ToList();
            ViewData["funcionario_id"] = new SelectList(funcionarios, "Id", "Nome");
            ViewData["epi_geral_id"] = new SelectList(_context.epi, "Id", "Nome", epi_funcionario.epi_geral_id);
            return View(epi_funcionario);
        }

        // POST: Cadastro/EpiFuncionario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,funcionario_id,epi_geral_id,Vencimento,Entrega")] epi_funcionario epi_funcionario)
        {
            if (id != epi_funcionario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(epi_funcionario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!epi_funcionarioExists(epi_funcionario.Id))
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
             var funcionarios = _context.funcionario.Where(f => f.geral.Tipo == "1")
            .Select(f => new
            {
                Id = f.Id,
                Nome = f.geral.Nome
            }).ToList();
            ViewData["funcionario_id"] = new SelectList(funcionarios, "Id", "Nome");
            ViewData["epi_geral_id"] = new SelectList(_context.epi, "Id", "Nome", epi_funcionario.epi_geral_id);
            return View(epi_funcionario);
        }

        // GET: Cadastro/EpiFuncionario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var epi_funcionario = await _context.epi_funcionario
                .Include(e => e.Funcionario)
                .Include(e => e.epi_geral)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (epi_funcionario == null)
            {
                return NotFound();
            }

            return View(epi_funcionario);
        }

        // POST: Cadastro/EpiFuncionario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var epi_funcionario = await _context.epi_funcionario.FindAsync(id);
            _context.epi_funcionario.Remove(epi_funcionario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool epi_funcionarioExists(int id)
        {
            return _context.epi_funcionario.Any(e => e.Id == id);
        }
    }
}
