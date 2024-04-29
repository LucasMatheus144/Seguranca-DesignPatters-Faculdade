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
    public class SalaDiarioController : Controller
    {
        private readonly ApaDbContext _context;

        public SalaDiarioController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/SalaDiario
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> Index()
        {
            var apaDbContext = _context.salas_diario.Include(s => s.Sala);
            return View(await apaDbContext.ToListAsync());
        }

        // GET: Cadastro/SalaDiario/Details/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salas_diario = await _context.salas_diario
                .Include(s => s.Sala)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salas_diario == null)
            {
                return NotFound();
            }

            return View(salas_diario);
        }

        // GET: Cadastro/SalaDiario/Create
        [Autorizacao(new[] { TipoUsuario.SuperUser, TipoUsuario.Admin, TipoUsuario.Funcionarios })]
        public IActionResult Create(int salaId = 0, string CodPeriodo = "0")
        {
            if (salaId != 0 && CodPeriodo !="0")
            {
                ViewData["sala_id"] = new SelectList(_context.sala, "Id", "Nome", salaId);
                ViewData["CodPeriodo"] = CodPeriodo;
            }
            else
            {
                ViewData["sala_id"] = new SelectList(_context.sala, "Id", "Nome");
            }
            
            return View();
        }

        // POST: Cadastro/SalaDiario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> Create([Bind("Id,sala_id,Periodo,Nota,periodoLabel")] salas_diario salas_diario)
        {
            if (ModelState.IsValid)
            {   
                _context.Add(salas_diario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["sala_id"] = new SelectList(_context.sala, "Id", "Nome", salas_diario.sala_id);
            return View(salas_diario);
        }

        // GET: Cadastro/SalaDiario/Edit/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salas_diario = await _context.salas_diario.FindAsync(id);
            if (salas_diario == null)
            {
                return NotFound();
            }
            ViewData["sala_id"] = new SelectList(_context.sala, "Id", "Nome", salas_diario.sala_id);
            return View(salas_diario);
        }

        // POST: Cadastro/SalaDiario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> Edit(int id, [Bind("Id,sala_id,Periodo,Nota")] salas_diario salas_diario)
        {
            if (id != salas_diario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salas_diario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!salas_diarioExists(salas_diario.Id))
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
            ViewData["sala_id"] = new SelectList(_context.sala, "Id", "Nome", salas_diario.sala_id);
            return View(salas_diario);
        }

        // GET: Cadastro/SalaDiario/Delete/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salas_diario = await _context.salas_diario
                .Include(s => s.Sala)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salas_diario == null)
            {
                return NotFound();
            }

            return View(salas_diario);
        }

        // POST: Cadastro/SalaDiario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salas_diario = await _context.salas_diario.FindAsync(id);
            _context.salas_diario.Remove(salas_diario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool salas_diarioExists(int id)
        {
            return _context.salas_diario.Any(e => e.Id == id);
        }
    }
}
