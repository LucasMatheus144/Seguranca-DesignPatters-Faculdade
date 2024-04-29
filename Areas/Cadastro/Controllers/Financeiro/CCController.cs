using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EspacoPotencial.Areas.Cadastro.Models.Financeiro;
using EspacoPotencial.Context;
using EspacoPotencial.Filters;
using EspacoPotencial.Models.Account;

namespace EspacoPotencial.Areas.Cadastro.Controllers.Financeiro
{
    [Area("Cadastro")]
    [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
    public class CCController : Controller
    {
        private readonly ApaDbContext _context;

        public CCController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/CC
        public async Task<IActionResult> Index()
        {
            return View(await _context.cc.ToListAsync());
        }

        // GET: Cadastro/CC/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cc = await _context.cc
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cc == null)
            {
                return NotFound();
            }

            return View(cc);
        }

        // GET: Cadastro/CC/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cadastro/CC/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ContaCorrenteVendaIdOrigem,ContaCorrenteVendaId,ContaCorrenteData,ContaCorrenteDebitoCredito,ContaCorrenteValor")] cc cc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cc);
        }

        // GET: Cadastro/CC/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cc = await _context.cc.FindAsync(id);
            if (cc == null)
            {
                return NotFound();
            }
            return View(cc);
        }

        // POST: Cadastro/CC/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ContaCorrenteVendaIdOrigem,ContaCorrenteVendaId,ContaCorrenteData,ContaCorrenteDebitoCredito,ContaCorrenteValor")] cc cc)
        {
            if (id != cc.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ccExists(cc.Id))
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
            return View(cc);
        }

        // GET: Cadastro/CC/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cc = await _context.cc
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cc == null)
            {
                return NotFound();
            }

            return View(cc);
        }

        // POST: Cadastro/CC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cc = await _context.cc.FindAsync(id);
            _context.cc.Remove(cc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ccExists(int id)
        {
            return _context.cc.Any(e => e.Id == id);
        }
    }
}
