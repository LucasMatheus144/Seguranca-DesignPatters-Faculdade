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
    public class AtestadoMedicoController : Controller
    {
        private readonly ApaDbContext _context;

        public AtestadoMedicoController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/AtestadoMedico
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Index()
        {
            var apaDbContext = _context.atestado_medico.Include(a => a.Cid).Include(a => a.funcionario).ThenInclude(g => g.geral);
            return View(await apaDbContext.ToListAsync());
        }

        // GET: Cadastro/AtestadoMedico/Details/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atestado_medico = await _context.atestado_medico
                .Include(a => a.Cid)
                .Include(a => a.funcionario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (atestado_medico == null)
            {
                return NotFound();
            }

            return View(atestado_medico);
        }

        // GET: Cadastro/AtestadoMedico/Create
        public IActionResult Create()
        {
            var funcionarios = _context.funcionario.Where(f => f.geral.Tipo == "1" && f.geral.Situacao == "1")
            .Select(f => new
            {
                Id = f.Id,
                Nome = f.geral.Nome
            }).ToList();
            ViewData["funcionario_id"] = new SelectList(funcionarios, "Id", "Nome");
             ViewData["cid_id"] = new SelectList(_context.cid, "Id", "Nome");
            return View();
        }

        // POST: Cadastro/AtestadoMedico/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,funcionario_id,cid_id,Data,Dias")] atestado_medico atestado_medico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(atestado_medico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
             var funcionarios = _context.funcionario.Where(f => f.geral.Tipo == "1" && f.geral.Situacao == "1")
            .Select(f => new
            {
                Id = f.Id,
                Nome = f.geral.Nome
            }).ToList();
            ViewData["funcionario_id"] = new SelectList(funcionarios, "Id", "Nome");
             ViewData["cid_id"] = new SelectList(_context.cid, "Id", "Nome");
            return View(atestado_medico);
        }

        // GET: Cadastro/AtestadoMedico/Edit/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atestado_medico = await _context.atestado_medico.FindAsync(id);
            if (atestado_medico == null)
            {
                return NotFound();
            }
             var funcionarios = _context.funcionario.Where(f => f.geral.Tipo == "1" && f.geral.Situacao == "1")
            .Select(f => new
            {
                Id = f.Id,
                Nome = f.geral.Nome
            }).ToList();
            ViewData["funcionario_id"] = new SelectList(funcionarios, "Id", "Nome");
             ViewData["cid_id"] = new SelectList(_context.cid, "Id", "Nome");
            return View(atestado_medico);
        }

        // POST: Cadastro/AtestadoMedico/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,funcionario_id,cid_id,Data,Dias")] atestado_medico atestado_medico)
        {
            if (id != atestado_medico.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(atestado_medico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!atestado_medicoExists(atestado_medico.Id))
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
             var funcionarios = _context.funcionario.Where(f => f.geral.Tipo == "1" && f.geral.Situacao == "1")
            .Select(f => new
            {
                Id = f.Id,
                Nome = f.geral.Nome
            }).ToList();
            ViewData["funcionario_id"] = new SelectList(funcionarios, "Id", "Nome");
             ViewData["cid_id"] = new SelectList(_context.cid, "Id", "Nome");
            return View(atestado_medico);
        }

        // GET: Cadastro/AtestadoMedico/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atestado_medico = await _context.atestado_medico
                .Include(a => a.Cid)
                .Include(a => a.funcionario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (atestado_medico == null)
            {
                return NotFound();
            }

            return View(atestado_medico);
        }

        // POST: Cadastro/AtestadoMedico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var atestado_medico = await _context.atestado_medico.FindAsync(id);
            _context.atestado_medico.Remove(atestado_medico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool atestado_medicoExists(int id)
        {
            return _context.atestado_medico.Any(e => e.Id == id);
        }
    }
}
