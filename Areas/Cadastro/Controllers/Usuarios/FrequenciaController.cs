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
    [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
    public class FrequenciaController : Controller
    {
        private readonly ApaDbContext _context;

        public FrequenciaController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/Frequencia
        public async Task<IActionResult> Index()
        {
           var apaDbContext = _context.frequencia
            .Include(f => f.Sala)
            /*.Include(f => f.UsuarioSala)
            .ThenInclude(g => g.Geral)*/
            .AsNoTracking();
            return View(await apaDbContext.ToListAsync());
        }

        // GET: Cadastro/Frequencia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var frequencia = await _context.frequencia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (frequencia == null)
            {
                return NotFound();
            }

            return View(frequencia);
        }

        // GET: Cadastro/Frequencia/Create
        public IActionResult Create()
        {
            ViewData["sala_id"] = new SelectList(_context.sala, "Id", "Nome");
            var usuarios = _context.usuario.Where(u => u.Geral.Tipo == "2" && u.Geral.Situacao == "1" )
            .Select(f => new
            {
                Id = f.Id,
                Nome = f.Geral.Nome
            }).ToList();
            ViewData["usuarios"] = new SelectList(usuarios, "Id", "Nome");
            ViewData["Data"] = DateTime.Now.Date;
            return View();
        }

        // POST: Cadastro/Frequencia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,sala_id,aluno_sala_id,Data,Registro,Observacao")] frequencia frequencia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(frequencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(frequencia);
        }

        // GET: Cadastro/Frequencia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var frequencia = await _context.frequencia.FindAsync(id);
            if (frequencia == null)
            {
                return NotFound();
            }
            return View(frequencia);
        }

        // POST: Cadastro/Frequencia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,sala_id,aluno_sala_id,Data,Registro,Observacao")] frequencia frequencia)
        {
            if (id != frequencia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(frequencia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!frequenciaExists(frequencia.Id))
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
            return View(frequencia);
        }

        // GET: Cadastro/Frequencia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var frequencia = await _context.frequencia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (frequencia == null)
            {
                return NotFound();
            }

            return View(frequencia);
        }

        // POST: Cadastro/Frequencia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var frequencia = await _context.frequencia.FindAsync(id);
            _context.frequencia.Remove(frequencia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool frequenciaExists(int id)
        {
            return _context.frequencia.Any(e => e.Id == id);
        }
    }
}
