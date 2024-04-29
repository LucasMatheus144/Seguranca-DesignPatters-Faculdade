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
    public class SalaController : Controller
    {
        private readonly ApaDbContext _context;

        public SalaController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/Sala
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Index()
        {
            var apaDbContext = _context.sala.Include(s => s.Funcionario).ThenInclude(fu => fu.geral).AsNoTracking();
            return View(await apaDbContext.ToListAsync());
        }

        // GET: Cadastro/Sala/Details/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sala = await _context.sala
                .Include(s => s.Funcionario)
                .ThenInclude(fu => fu.geral)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sala == null)
            {
                return NotFound();
            }

            return View(sala);
        }

        // GET: Cadastro/Sala/Create
        [Autorizacao(new[] { TipoUsuario.SuperUser, TipoUsuario.Admin })]
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

            ViewData["funcionario_nome"] = new SelectList(funcionarios, "Id", "Nome");
            
            return View();
        }


        // POST: Cadastro/Sala/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Create([Bind("Id,Periodo,Nome,funcionario_id")] sala sala)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sala);
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

            ViewData["funcionario_nome"] = new SelectList(funcionarios, "Id", "Nome");
            return View(sala);
        }

        // GET: Cadastro/Sala/Edit/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sala = await _context.sala.FindAsync(id);
            if (sala == null)
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

            ViewData["funcionario_nome"] = new SelectList(funcionarios, "Id", "Nome");
            return View(sala);
        }

        // POST: Cadastro/Sala/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Periodo,Nome,funcionario_id")] sala sala)
        {
            if (id != sala.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sala);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!salaExists(sala.Id))
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

            ViewData["funcionario_nome"] = new SelectList(funcionarios, "Id", "Nome");
            return View(sala);
        }

        // GET: Cadastro/Sala/Delete/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sala = await _context.sala
                .Include(s => s.Funcionario)
                .ThenInclude(fu => fu.geral)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sala == null)
            {
                return NotFound();
            }

            return View(sala);
        }

        // POST: Cadastro/Sala/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sala = await _context.sala.FindAsync(id);
            _context.sala.Remove(sala);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool salaExists(int id)
        {
            return _context.sala.Any(e => e.Id == id);
        }
    }
}
