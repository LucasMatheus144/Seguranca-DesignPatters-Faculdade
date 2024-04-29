using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EspacoPotencial.Areas.Cadastro.Models.Usuarios;
using EspacoPotencial.Context;
using EspacoPotencial.Filters;
using EspacoPotencial.Models.Account;

namespace EspacoPotencial.Areas.Cadastro.Controllers.Usuarios
{
    [Area("Cadastro")]
    public class EscolaController : Controller
    {
        private readonly ApaDbContext _context;

        public EscolaController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/Escola
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Index()
        {
            return View(await _context.escolas.ToListAsync());
        }

        // GET: Cadastro/Escola/Details/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var escolas = await _context.escolas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (escolas == null)
            {
                return NotFound();
            }

            return View(escolas);
        }

        // GET: Cadastro/Escola/Create
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cadastro/Escola/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Create([Bind("Id,Nome,Cidade,Telefone")] escolas escolas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(escolas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(escolas);
        }

        // GET: Cadastro/Escola/Edit/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var escolas = await _context.escolas.FindAsync(id);
            if (escolas == null)
            {
                return NotFound();
            }
            return View(escolas);
        }

        // POST: Cadastro/Escola/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Cidade,Telefone")] escolas escolas)
        {
            if (id != escolas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(escolas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!escolasExists(escolas.Id))
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
            return View(escolas);
        }

        // GET: Cadastro/Escola/Delete/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var escolas = await _context.escolas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (escolas == null)
            {
                return NotFound();
            }

            return View(escolas);
        }

        // POST: Cadastro/Escola/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var escolas = await _context.escolas.FindAsync(id);
            _context.escolas.Remove(escolas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool escolasExists(int id)
        {
            return _context.escolas.Any(e => e.Id == id);
        }
    }
}
