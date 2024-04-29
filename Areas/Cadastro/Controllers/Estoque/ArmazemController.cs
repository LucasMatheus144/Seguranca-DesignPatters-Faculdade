using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EspacoPotencial.Areas.Cadastro.Models.Financeiro;
using EspacoPotencial.Context;

namespace EspacoPotencial.Areas.Cadastro.Controllers.Estoque
{
    [Area("Cadastro")]
    public class ArmazemController : Controller
    {
        private readonly ApaDbContext _context;

        public ArmazemController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/Armazem
        public async Task<IActionResult> Index()
        {
            var apaDbContext = _context.armazem.Include(a => a.funcionario).ThenInclude(d => d.geral).Include(a => a.tipo);
            return View(await apaDbContext.ToListAsync());
        }

        // GET: Cadastro/Armazem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var armazem = await _context.armazem
                .Include(a => a.funcionario).ThenInclude(d => d.geral)
                .Include(a => a.tipo)
                .FirstOrDefaultAsync(m => m.id == id);
            if (armazem == null)
            {
                return NotFound();
            }

            return View(armazem);
        }

        // GET: Cadastro/Armazem/Create
        public IActionResult Create()
        {
             var funcionarios = _context.cargo_funcionario
            .Where(cf => cf.Situacao == "1" && cf.cargos_id == 3)
            .Join(
                _context.funcionario,
                cf => cf.funcionario_id,
                f => f.Id,
                (cf, f) => new
                {
                    Id = f.Id,
                    Nome = f.geral.Nome
                })
            .ToList();

            ViewData["funcionario_id"] = new SelectList(funcionarios, "Id", "Nome");
            ViewData["tipo_armazem"] = new SelectList(_context.Set<tipo>(), "id", "nome");
            return View();
        }

        // POST: Cadastro/Armazem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,funcionario_id,nome,qntdelimite,tipo_armazem")] armazem armazem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(armazem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var funcionarios = _context.cargo_funcionario
            .Where(cf => cf.Situacao == "1" && cf.cargos_id == 5)
            .Join(
                _context.funcionario,
                cf => cf.funcionario_id,
                f => f.Id,
                (cf, f) => new
                {
                    Id = f.Id,
                    Nome = f.geral.Nome
                })
            .ToList();

            ViewData["funcionario_id"] = new SelectList(funcionarios, "Id", "Nome",armazem.funcionario_id);
            ViewData["tipo_armazem"] = new SelectList(_context.Set<tipo>(), "id", "nome", armazem.tipo_armazem);
            return View(armazem);
        }

        // GET: Cadastro/Armazem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var armazem = await _context.armazem.FindAsync(id);
            if (armazem == null)
            {
                return NotFound();
            }
            var funcionarios = _context.cargo_funcionario
            .Where(cf => cf.Situacao == "1" && cf.cargos_id == 5)
            .Join(
                _context.funcionario,
                cf => cf.funcionario_id,
                f => f.Id,
                (cf, f) => new
                {
                    Id = f.Id,
                    Nome = f.geral.Nome
                })
            .ToList();

            ViewData["funcionario_id"] = new SelectList(funcionarios, "Id", "Nome",armazem.funcionario_id);
            ViewData["tipo_armazem"] = new SelectList(_context.Set<tipo>(), "id", "nome", armazem.tipo_armazem);
            return View(armazem);
        }

        // POST: Cadastro/Armazem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,funcionario_id,nome,qntdelimite,tipo_armazem")] armazem armazem)
        {
            if (id != armazem.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(armazem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!armazemExists(armazem.id))
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
           var funcionarios = _context.cargo_funcionario
            .Where(cf => cf.Situacao == "1" && cf.cargos_id == 5)
            .Join(
                _context.funcionario,
                cf => cf.funcionario_id,
                f => f.Id,
                (cf, f) => new
                {
                    Id = f.Id,
                    Nome = f.geral.Nome
                })
            .ToList();

            ViewData["funcionario_id"] = new SelectList(funcionarios, "Id", "Nome",armazem.funcionario_id);
            ViewData["tipo_armazem"] = new SelectList(_context.Set<tipo>(), "id", "nome", armazem.tipo_armazem);
            return View(armazem);
        }

        // GET: Cadastro/Armazem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var armazem = await _context.armazem
                .Include(a => a.funcionario).ThenInclude(d => d.geral)
                .Include(a => a.tipo)
                .FirstOrDefaultAsync(m => m.id == id);
            if (armazem == null)
            {
                return NotFound();
            }

            return View(armazem);
        }

        // POST: Cadastro/Armazem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var armazem = await _context.armazem.FindAsync(id);
            _context.armazem.Remove(armazem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool armazemExists(int id)
        {
            return _context.armazem.Any(e => e.id == id);
        }
    }
}
