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
    public class MotivoDesligamentoUsuarioController : Controller
    {
        private readonly ApaDbContext _context;

        public MotivoDesligamentoUsuarioController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/MotivoDesligamentoUsuario
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Index()
        {
            return View(await _context.desligamento_motivos_usuario.ToListAsync());
        }

        // GET: Cadastro/MotivoDesligamentoUsuario/Details/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var desligamento_motivos_usuario = await _context.desligamento_motivos_usuario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (desligamento_motivos_usuario == null)
            {
                return NotFound();
            }

            return View(desligamento_motivos_usuario);
        }

        // GET: Cadastro/MotivoDesligamentoUsuario/Create
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cadastro/MotivoDesligamentoUsuario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Create([Bind("Id,Motivo")] desligamento_motivos_usuario desligamento_motivos_usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(desligamento_motivos_usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(desligamento_motivos_usuario);
        }

        // GET: Cadastro/MotivoDesligamentoUsuario/Edit/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var desligamento_motivos_usuario = await _context.desligamento_motivos_usuario.FindAsync(id);
            if (desligamento_motivos_usuario == null)
            {
                return NotFound();
            }
            return View(desligamento_motivos_usuario);
        }

        // POST: Cadastro/MotivoDesligamentoUsuario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Motivo")] desligamento_motivos_usuario desligamento_motivos_usuario)
        {
            if (id != desligamento_motivos_usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(desligamento_motivos_usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!desligamento_motivos_usuarioExists(desligamento_motivos_usuario.Id))
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
            return View(desligamento_motivos_usuario);
        }

        // GET: Cadastro/MotivoDesligamentoUsuario/Delete/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var desligamento_motivos_usuario = await _context.desligamento_motivos_usuario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (desligamento_motivos_usuario == null)
            {
                return NotFound();
            }

            return View(desligamento_motivos_usuario);
        }

        // POST: Cadastro/MotivoDesligamentoUsuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var desligamento_motivos_usuario = await _context.desligamento_motivos_usuario.FindAsync(id);
            _context.desligamento_motivos_usuario.Remove(desligamento_motivos_usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool desligamento_motivos_usuarioExists(int id)
        {
            return _context.desligamento_motivos_usuario.Any(e => e.Id == id);
        }
    }
}
