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
    public class GrupoPermitidoController : Controller
    {
        private readonly ApaDbContext _context;

        public GrupoPermitidoController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/GrupoPermitido
        public async Task<IActionResult> Index()
        {
            return View(await _context.grupo_permitido.ToListAsync());
        }

        // GET: Cadastro/GrupoPermitido/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupo_permitido = await _context.grupo_permitido
                .FirstOrDefaultAsync(m => m.grp_id == id);
            if (grupo_permitido == null)
            {
                return NotFound();
            }

            return View(grupo_permitido);
        }

        // GET: Cadastro/GrupoPermitido/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cadastro/GrupoPermitido/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("grp_id,grp_descricao")] grupo_permitido grupo_permitido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grupo_permitido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(grupo_permitido);
        }

        // GET: Cadastro/GrupoPermitido/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupo_permitido = await _context.grupo_permitido.FindAsync(id);
            if (grupo_permitido == null)
            {
                return NotFound();
            }
            return View(grupo_permitido);
        }

        // POST: Cadastro/GrupoPermitido/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("grp_id,grp_descricao")] grupo_permitido grupo_permitido)
        {
            if (id != grupo_permitido.grp_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grupo_permitido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!grupo_permitidoExists(grupo_permitido.grp_id))
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
            return View(grupo_permitido);
        }

        // GET: Cadastro/GrupoPermitido/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupo_permitido = await _context.grupo_permitido
                .FirstOrDefaultAsync(m => m.grp_id == id);
            if (grupo_permitido == null)
            {
                return NotFound();
            }

            return View(grupo_permitido);
        }

        // POST: Cadastro/GrupoPermitido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grupo_permitido = await _context.grupo_permitido.FindAsync(id);
            _context.grupo_permitido.Remove(grupo_permitido);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool grupo_permitidoExists(int id)
        {
            return _context.grupo_permitido.Any(e => e.grp_id == id);
        }
    }
}
