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
    public class MotivoDesligamentoController : Controller
    {
        private readonly ApaDbContext _context;

        public MotivoDesligamentoController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/MotivoDesligamento
        public async Task<IActionResult> Index()
        {
            return View(await _context.motivo_desligamento.ToListAsync());
        }

        // GET: Cadastro/MotivoDesligamento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motivo_desligamento = await _context.motivo_desligamento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (motivo_desligamento == null)
            {
                return NotFound();
            }

            return View(motivo_desligamento);
        }

        // GET: Cadastro/MotivoDesligamento/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cadastro/MotivoDesligamento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descricao")] motivo_desligamento motivo_desligamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(motivo_desligamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(motivo_desligamento);
        }

        // GET: Cadastro/MotivoDesligamento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motivo_desligamento = await _context.motivo_desligamento.FindAsync(id);
            if (motivo_desligamento == null)
            {
                return NotFound();
            }
            return View(motivo_desligamento);
        }

        // POST: Cadastro/MotivoDesligamento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao")] motivo_desligamento motivo_desligamento)
        {
            if (id != motivo_desligamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(motivo_desligamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!motivo_desligamentoExists(motivo_desligamento.Id))
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
            return View(motivo_desligamento);
        }

        // GET: Cadastro/MotivoDesligamento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motivo_desligamento = await _context.motivo_desligamento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (motivo_desligamento == null)
            {
                return NotFound();
            }

            return View(motivo_desligamento);
        }

        // POST: Cadastro/MotivoDesligamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var motivo_desligamento = await _context.motivo_desligamento.FindAsync(id);
            _context.motivo_desligamento.Remove(motivo_desligamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool motivo_desligamentoExists(int id)
        {
            return _context.motivo_desligamento.Any(e => e.Id == id);
        }
    }
}
