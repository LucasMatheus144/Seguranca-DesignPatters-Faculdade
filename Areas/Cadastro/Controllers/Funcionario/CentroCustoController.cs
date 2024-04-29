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
    public class CentroCustoController : Controller
    {
        private readonly ApaDbContext _context;

        public CentroCustoController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/CentroCusto
        public async Task<IActionResult> Index()
        {
            return View(await _context.centro_custo.ToListAsync());
        }

        // GET: Cadastro/CentroCusto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centro_custo = await _context.centro_custo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (centro_custo == null)
            {
                return NotFound();
            }

            return View(centro_custo);
        }

        // GET: Cadastro/CentroCusto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cadastro/CentroCusto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] centro_custo centro_custo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(centro_custo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(centro_custo);
        }

        // GET: Cadastro/CentroCusto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centro_custo = await _context.centro_custo.FindAsync(id);
            if (centro_custo == null)
            {
                return NotFound();
            }
            return View(centro_custo);
        }

        // POST: Cadastro/CentroCusto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] centro_custo centro_custo)
        {
            if (id != centro_custo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(centro_custo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!centro_custoExists(centro_custo.Id))
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
            return View(centro_custo);
        }

        // GET: Cadastro/CentroCusto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centro_custo = await _context.centro_custo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (centro_custo == null)
            {
                return NotFound();
            }

            return View(centro_custo);
        }

        // POST: Cadastro/CentroCusto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var centro_custo = await _context.centro_custo.FindAsync(id);
            _context.centro_custo.Remove(centro_custo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool centro_custoExists(int id)
        {
            return _context.centro_custo.Any(e => e.Id == id);
        }
    }
}
