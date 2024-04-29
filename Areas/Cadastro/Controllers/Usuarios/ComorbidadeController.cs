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
    public class ComorbidadeController : Controller
    {
        private readonly ApaDbContext _context;

        public ComorbidadeController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/Comorbidade
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Index()
        {
            return View(await _context.comorbidade.ToListAsync());
        }

        // GET: Cadastro/Comorbidade/Details/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comorbidade = await _context.comorbidade
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comorbidade == null)
            {
                return NotFound();
            }

            return View(comorbidade);
        }

        // GET: Cadastro/Comorbidade/Create
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cadastro/Comorbidade/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Create([Bind("Id,Descricao")] comorbidade comorbidade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comorbidade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comorbidade);
        }

        // GET: Cadastro/Comorbidade/Edit/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comorbidade = await _context.comorbidade.FindAsync(id);
            if (comorbidade == null)
            {
                return NotFound();
            }
            return View(comorbidade);
        }

        // POST: Cadastro/Comorbidade/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao")] comorbidade comorbidade)
        {
            if (id != comorbidade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comorbidade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!comorbidadeExists(comorbidade.Id))
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
            return View(comorbidade);
        }

        // GET: Cadastro/Comorbidade/Delete/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comorbidade = await _context.comorbidade
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comorbidade == null)
            {
                return NotFound();
            }

            return View(comorbidade);
        }

        // POST: Cadastro/Comorbidade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comorbidade = await _context.comorbidade.FindAsync(id);
            _context.comorbidade.Remove(comorbidade);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool comorbidadeExists(int id)
        {
            return _context.comorbidade.Any(e => e.Id == id);
        }
    }
}
