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
    public class ConvenioController : Controller
    {
        private readonly ApaDbContext _context;

        public ConvenioController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/Convenio
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Index()
        {
            return View(await _context.convenio.ToListAsync());
        }

        // GET: Cadastro/Convenio/Details/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var convenio = await _context.convenio
                .FirstOrDefaultAsync(m => m.Id == id);
            if (convenio == null)
            {
                return NotFound();
            }

            return View(convenio);
        }

        // GET: Cadastro/Convenio/Create
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cadastro/Convenio/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Create([Bind("Id,Nome")] convenio convenio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(convenio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(convenio);
        }

        // GET: Cadastro/Convenio/Edit/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var convenio = await _context.convenio.FindAsync(id);
            if (convenio == null)
            {
                return NotFound();
            }
            return View(convenio);
        }

        // POST: Cadastro/Convenio/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] convenio convenio)
        {
            if (id != convenio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(convenio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!convenioExists(convenio.Id))
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
            return View(convenio);
        }

        // GET: Cadastro/Convenio/Delete/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var convenio = await _context.convenio
                .FirstOrDefaultAsync(m => m.Id == id);
            if (convenio == null)
            {
                return NotFound();
            }

            return View(convenio);
        }

        // POST: Cadastro/Convenio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var convenio = await _context.convenio.FindAsync(id);
            _context.convenio.Remove(convenio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool convenioExists(int id)
        {
            return _context.convenio.Any(e => e.Id == id);
        }
    }
}
