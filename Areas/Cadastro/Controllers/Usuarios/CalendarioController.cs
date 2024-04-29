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
    public class CalendarioController : Controller
    {
        private readonly ApaDbContext _context;

        public CalendarioController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/Calendario
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> Index()
        {
            var apaDbContext = _context.calendario.Include(c => c.Datas).Include(c => c.Salas);
            return View(await apaDbContext.ToListAsync());
        }

        // GET: Cadastro/Calendario/Details/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calendario = await _context.calendario
                .Include(c => c.Datas)
                .Include(c => c.Salas)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (calendario == null)
            {
                return NotFound();
            }

            return View(calendario);
        }

        // GET: Cadastro/Calendario/Create
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public IActionResult Create()
        {
            ViewData["datas_id"] = new SelectList(_context.datas, "Id", "Descritivo");
            ViewData["sala_id"] = new SelectList(_context.sala, "Id", "Nome");
            return View();
        }

        // POST: Cadastro/Calendario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> Create([Bind("Id,sala_id,datas_id")] calendario calendario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calendario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
             ViewData["datas_id"] = new SelectList(_context.datas, "Id", "Descritivo", calendario.datas_id);
            ViewData["sala_id"] = new SelectList(_context.sala, "Id", "Nome", calendario.sala_id);
            return View(calendario);
        }

        // GET: Cadastro/Calendario/Edit/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calendario = await _context.calendario.FindAsync(id);
            if (calendario == null)
            {
                return NotFound();
            }
            ViewData["datas_id"] = new SelectList(_context.datas, "Id", "Descritivo", calendario.datas_id);
            ViewData["sala_id"] = new SelectList(_context.sala, "Id", "Nome", calendario.sala_id);
            return View(calendario);
        }

        // POST: Cadastro/Calendario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> Edit(int id, [Bind("Id,sala_id,datas_id")] calendario calendario)
        {
            if (id != calendario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calendario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!calendarioExists(calendario.Id))
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
            ViewData["datas_id"] = new SelectList(_context.datas, "Id", "Descritivo", calendario.datas_id);
            ViewData["sala_id"] = new SelectList(_context.sala, "Id", "Nome", calendario.sala_id);

            return View(calendario);
        }

        // GET: Cadastro/Calendario/Delete/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calendario = await _context.calendario
                .Include(c => c.Datas)
                .Include(c => c.Salas)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (calendario == null)
            {
                return NotFound();
            }

            return View(calendario);
        }

        // POST: Cadastro/Calendario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var calendario = await _context.calendario.FindAsync(id);
            _context.calendario.Remove(calendario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool calendarioExists(int id)
        {
            return _context.calendario.Any(e => e.Id == id);
        }
    }
}
