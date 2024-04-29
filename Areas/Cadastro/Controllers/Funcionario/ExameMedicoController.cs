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
    public class ExameMedicoController : Controller
    {
        private readonly ApaDbContext _context;

        public ExameMedicoController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/ExameMedico
        public async Task<IActionResult> Index()
        {
            var apaDbContext = _context.exame_medico.Include(e => e.Funcionario).ThenInclude(g => g.geral);
            return View(await apaDbContext.ToListAsync());
        }

        // GET: Cadastro/ExameMedico/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exame_medico = await _context.exame_medico
                .Include(e => e.Funcionario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exame_medico == null)
            {
                return NotFound();
            }

            return View(exame_medico);
        }

        // GET: Cadastro/ExameMedico/Create
        public IActionResult Create()
        {
             var funcionarios = _context.funcionario.Where(f => f.geral.Tipo == "1")
            .Select(f => new
            {
                Id = f.Id,
                Nome = f.geral.Nome
            }).ToList();
            ViewData["funcionario_id"] = new SelectList(funcionarios, "Id", "Nome");
            return View();
        }

        // POST: Cadastro/ExameMedico/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,funcionario_id,Exame,DataAgendada,DataExecuta")] exame_medico exame_medico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exame_medico);
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
            return View(exame_medico);
        }

        // GET: Cadastro/ExameMedico/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exame_medico = await _context.exame_medico.FindAsync(id);
            if (exame_medico == null)
            {
                return NotFound();
            }
             var funcionarios = _context.funcionario.Where(f => f.geral.Tipo == "1")
            .Select(f => new
            {
                Id = f.Id,
                Nome = f.geral.Nome
            }).ToList();
            ViewData["funcionario_id"] = new SelectList(funcionarios, "Id", "Nome");
            return View(exame_medico);
        }

        // POST: Cadastro/ExameMedico/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,funcionario_id,Exame,DataAgendada,DataExecuta")] exame_medico exame_medico)
        {
            if (id != exame_medico.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exame_medico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!exame_medicoExists(exame_medico.Id))
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
             var funcionarios = _context.funcionario.Where(f => f.geral.Tipo == "1")
            .Select(f => new
            {
                Id = f.Id,
                Nome = f.geral.Nome
            }).ToList();
            ViewData["funcionario_id"] = new SelectList(funcionarios, "Id", "Nome");
            return View(exame_medico);
        }

        // GET: Cadastro/ExameMedico/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exame_medico = await _context.exame_medico
                .Include(e => e.Funcionario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exame_medico == null)
            {
                return NotFound();
            }

            return View(exame_medico);
        }

        // POST: Cadastro/ExameMedico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exame_medico = await _context.exame_medico.FindAsync(id);
            _context.exame_medico.Remove(exame_medico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool exame_medicoExists(int id)
        {
            return _context.exame_medico.Any(e => e.Id == id);
        }
    }
}
