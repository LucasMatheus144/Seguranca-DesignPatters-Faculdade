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
    public class EducadorSalaController : Controller
    {
        private readonly ApaDbContext _context;

        public EducadorSalaController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/EducadorSala
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> Index()
        {
            var apaDbContext = _context.educador_sala.Include(e => e.Funcionario).ThenInclude(e => e.geral).Include(e => e.Sala);
            return View(await apaDbContext.ToListAsync());
        }

        // GET: Cadastro/EducadorSala/Details/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educador_sala = await _context.educador_sala
                .Include(e => e.Funcionario)
                .ThenInclude(d => d.geral)
                .Include(e => e.Sala)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (educador_sala == null)
            {
                return NotFound();
            }

            return View(educador_sala);
        }

        // GET: Cadastro/EducadorSala/Create
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public IActionResult Create()
        {
            var funcionarios = _context.cargo_funcionario
            .Where(cf => cf.Situacao == "1")
            .Join(
                _context.funcionario,
                cf => cf.funcionario_id,
                f => f.Id,
                (cf, f) => new
                {
                    Id = f.Id,
                    Nome = f.geral.Nome
                })
            .ToList();

            ViewData["funcionario_id"] = new SelectList(funcionarios, "Id", "Nome");
            ViewData["sala_id"] = new SelectList(_context.sala, "Id", "Nome");
            return View();
        }

        // POST: Cadastro/EducadorSala/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> Create([Bind("Id,funcionario_id,sala_id,Inicio,Final")] educador_sala educador_sala)
        {
            if (ModelState.IsValid)
            {   
                _context.Add(educador_sala);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           var funcionarios = _context.cargo_funcionario
            .Where(cf => cf.Situacao == "1")
            .Join(
                _context.funcionario,
                cf => cf.funcionario_id,
                f => f.Id,
                (cf, f) => new
                {
                    Id = f.Id,
                    Nome = f.geral.Nome
                })
            .ToList();

            ViewData["funcionario_id"] = new SelectList(funcionarios, "Id", "Nome");
            ViewData["sala_id"] = new SelectList(_context.sala, "Id", "Nome");
            return View(educador_sala);
        }

        // GET: Cadastro/EducadorSala/Edit/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educador_sala = await _context.educador_sala.FindAsync(id);
            if (educador_sala == null)
            {
                return NotFound();
            }
           var funcionarios = _context.cargo_funcionario
            .Where(cf => cf.Situacao == "1")
            .Join(
                _context.funcionario,
                cf => cf.funcionario_id,
                f => f.Id,
                (cf, f) => new
                {
                    Id = f.Id,
                    Nome = f.geral.Nome
                })
            .ToList();

            ViewData["funcionario_id"] = new SelectList(funcionarios, "Id", "Nome");
            ViewData["sala_id"] = new SelectList(_context.sala, "Id", "Nome");
            return View(educador_sala);
        }

        // POST: Cadastro/EducadorSala/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> Edit(int id, [Bind("Id,funcionario_id,sala_id,Inicio,Final")] educador_sala educador_sala)
        {
            if (id != educador_sala.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(educador_sala);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!educador_salaExists(educador_sala.Id))
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
           var funcionarios = _context.cargo_funcionario
            .Where(cf => cf.Situacao == "1")
            .Join(
                _context.funcionario,
                cf => cf.funcionario_id,
                f => f.Id,
                (cf, f) => new
                {
                    Id = f.Id,
                    Nome = f.geral.Nome
                })
            .ToList();

            ViewData["funcionario_id"] = new SelectList(funcionarios, "Id", "Nome");
            ViewData["sala_id"] = new SelectList(_context.sala, "Id", "Nome");
            return View(educador_sala);
        }

        // GET: Cadastro/EducadorSala/Delete/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educador_sala = await _context.educador_sala
                .Include(e => e.Funcionario)
                .ThenInclude(d => d.geral)
                .Include(e => e.Sala)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (educador_sala == null)
            {
                return NotFound();
            }

            return View(educador_sala);
        }

        // POST: Cadastro/EducadorSala/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var educador_sala = await _context.educador_sala.FindAsync(id);
            _context.educador_sala.Remove(educador_sala);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool educador_salaExists(int id)
        {
            return _context.educador_sala.Any(e => e.Id == id);
        }
    }
}
