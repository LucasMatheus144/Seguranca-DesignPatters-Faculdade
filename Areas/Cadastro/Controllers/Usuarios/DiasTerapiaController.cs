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
    public class DiasTerapiaController : Controller
    {
        private readonly ApaDbContext _context;

        public DiasTerapiaController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/DiasTerapia
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> Index()
        {
            var apaDbContext = _context.dias_terapia.Include(d => d.Geral).Where(d => d.Geral.Tipo == "2" &&  d.Geral.Situacao == "1");
            return View(await apaDbContext.ToListAsync());
        }

        // GET: Cadastro/DiasTerapia/Details/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dias_terapia = await _context.dias_terapia
                .Include(d => d.Geral)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dias_terapia == null)
            {
                return NotFound();
            }

            return View(dias_terapia);
        }

        // GET: Cadastro/DiasTerapia/Create
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public IActionResult Create()
        {
            ViewData["geral_id"] = new SelectList(_context.geral.Where(d => d.Tipo == "2" &&  d.Situacao == "1"), "Id", "Nome");
            return View();
        }

        // POST: Cadastro/DiasTerapia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser, TipoUsuario.Admin, TipoUsuario.Funcionarios })]
        public async Task<IActionResult> Create([Bind("Id,geral_id,DataInicial,DataFinal,DiaSemana,segunda,terca,quarta,quinta,sexta")] dias_terapia dias_terapia)
        {
            if (ModelState.IsValid)
            {
                // Verificar se já existe um registro com o mesmo geral_id e DataInicial
                var existingRecord = await _context.dias_terapia
                    .FirstOrDefaultAsync(d => d.geral_id == dias_terapia.geral_id && d.DataInicial == dias_terapia.DataInicial);

                if (existingRecord != null)
                {
                    // Registro duplicado encontrado
                    ModelState.AddModelError(string.Empty, "Já existe um registro para o mesmo usuário com a mesma data inicial.");
                    ViewData["geral_id"] = new SelectList(_context.geral, "Id", "Nome", dias_terapia.geral_id);
                    return View(dias_terapia);
                }

                // Não há registro duplicado, então adiciona o novo registro
                _context.Add(dias_terapia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["geral_id"] = new SelectList(_context.geral, "Id", "Nome", dias_terapia.geral_id);
            return View(dias_terapia);
        }


        // GET: Cadastro/DiasTerapia/Edit/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dias_terapia = await _context.dias_terapia.FindAsync(id);
            if (dias_terapia == null)
            {
                return NotFound();
            }
            ViewData["geral_id"] = new SelectList(_context.geral, "Id", "Nome", dias_terapia.geral_id);
            return View(dias_terapia);
        }

        // POST: Cadastro/DiasTerapia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> Edit(int id, [Bind("Id,geral_id,DataInicial,DataFinal,DiaSemana,segunda,terca,quarta,quinta,sexta")] dias_terapia dias_terapia)
        {
            if (id != dias_terapia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dias_terapia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!dias_terapiaExists(dias_terapia.Id))
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
            ViewData["geral_id"] = new SelectList(_context.geral, "Id", "Nome", dias_terapia.geral_id);
            return View(dias_terapia);
        }

        // GET: Cadastro/DiasTerapia/Delete/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dias_terapia = await _context.dias_terapia
                .Include(d => d.Geral)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dias_terapia == null)
            {
                return NotFound();
            }

            return View(dias_terapia);
        }

        // POST: Cadastro/DiasTerapia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dias_terapia = await _context.dias_terapia.FindAsync(id);
            _context.dias_terapia.Remove(dias_terapia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool dias_terapiaExists(int id)
        {
            return _context.dias_terapia.Any(e => e.Id == id);
        }
    }
}
