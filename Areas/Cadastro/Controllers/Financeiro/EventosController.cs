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
    public class EventosController : Controller
    {
        private readonly ApaDbContext _context;

        public EventosController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/Eventos
        public async Task<IActionResult> Index()
        {
            return View(await _context.eventos.ToListAsync());
        }

        // GET: Cadastro/Eventos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventos = await _context.eventos
                .FirstOrDefaultAsync(m => m.eventos_id == id);
            if (eventos == null)
            {
                return NotFound();
            }

            return View(eventos);
        }

        // GET: Cadastro/Eventos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cadastro/Eventos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("eventos_id,evento_nome,evento_data_ini,evento_data_fin")] eventos eventos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eventos);
        }

        // GET: Cadastro/Eventos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventos = await _context.eventos.FindAsync(id);
            if (eventos == null)
            {
                return NotFound();
            }
            return View(eventos);
        }

        // POST: Cadastro/Eventos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("eventos_id,evento_nome,evento_data_ini,evento_data_fin")] eventos eventos)
        {
            if (id != eventos.eventos_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!eventosExists(eventos.eventos_id))
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
            return View(eventos);
        }

        // GET: Cadastro/Eventos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventos = await _context.eventos
                .FirstOrDefaultAsync(m => m.eventos_id == id);
            if (eventos == null)
            {
                return NotFound();
            }

            return View(eventos);
        }

        // POST: Cadastro/Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventos = await _context.eventos.FindAsync(id);
            _context.eventos.Remove(eventos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool eventosExists(int id)
        {
            return _context.eventos.Any(e => e.eventos_id == id);
        }
    }
}
