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
    public class EpiController : Controller
    {
        private readonly ApaDbContext _context;

        public EpiController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/Epi
        public async Task<IActionResult> Index()
        {
            return View(await _context.epi.ToListAsync());
        }

        // GET: Cadastro/Epi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var epi = await _context.epi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (epi == null)
            {
                return NotFound();
            }

            return View(epi);
        }

        // GET: Cadastro/Epi/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cadastro/Epi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Validade")] epi epi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(epi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(epi);
        }

        // GET: Cadastro/Epi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var epi = await _context.epi.FindAsync(id);
            if (epi == null)
            {
                return NotFound();
            }
            return View(epi);
        }

        // POST: Cadastro/Epi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Validade")] epi epi)
        {
            if (id != epi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(epi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!epiExists(epi.Id))
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
            return View(epi);
        }

        // GET: Cadastro/Epi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var epi = await _context.epi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (epi == null)
            {
                return NotFound();
            }

            return View(epi);
        }

        // POST: Cadastro/Epi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var epi = await _context.epi.FindAsync(id);
            _context.epi.Remove(epi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool epiExists(int id)
        {
            return _context.epi.Any(e => e.Id == id);
        }
    }
}
