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
    public class CidController : Controller
    {
        private readonly ApaDbContext _context;

        public CidController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/Cid
        public async Task<IActionResult> Index()
        {
            return View(await _context.cid.ToListAsync());
        }

        // GET: Cadastro/Cid/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cid = await _context.cid
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cid == null)
            {
                return NotFound();
            }

            return View(cid);
        }

        // GET: Cadastro/Cid/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cadastro/Cid/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] cid cid)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cid);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cid);
        }

        // GET: Cadastro/Cid/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cid = await _context.cid.FindAsync(id);
            if (cid == null)
            {
                return NotFound();
            }
            return View(cid);
        }

        // POST: Cadastro/Cid/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] cid cid)
        {
            if (id != cid.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cid);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!cidExists(cid.Id))
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
            return View(cid);
        }

        // GET: Cadastro/Cid/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cid = await _context.cid
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cid == null)
            {
                return NotFound();
            }

            return View(cid);
        }

        // POST: Cadastro/Cid/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cid = await _context.cid.FindAsync(id);
            _context.cid.Remove(cid);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool cidExists(int id)
        {
            return _context.cid.Any(e => e.Id == id);
        }
    }
}
