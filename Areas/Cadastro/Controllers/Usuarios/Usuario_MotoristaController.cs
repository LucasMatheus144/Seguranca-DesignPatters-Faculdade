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
    public class Usuario_MotoristaController : Controller
    {
        private readonly ApaDbContext _context;

        public Usuario_MotoristaController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/Usuario_Motorista
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Index()
        {
            var apaDbContext = _context.usuario_motorista.Include(u => u.Motorista).Include(u => u.Usuario).ThenInclude(D => D.Geral);
            return View(await apaDbContext.ToListAsync());
        }

        // GET: Cadastro/Usuario_Motorista/Details/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario_motorista = await _context.usuario_motorista
                .Include(u => u.Motorista)
                .Include(u => u.Usuario)
                .ThenInclude(D => D.Geral)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario_motorista == null)
            {
                return NotFound();
            }

            return View(usuario_motorista);
        }

        // GET: Cadastro/Usuario_Motorista/Create
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public IActionResult Create()
        {
            var usuarios = _context.usuario.Where(u => u.Geral.Tipo == "2" && u.Geral.Situacao == "1" )
                .Select(f => new
                {
                    Id = f.Id,
                    Nome = f.Geral.Nome
                }).ToList();
            ViewData["usuario_id"] = new SelectList(usuarios, "Id", "Nome");
            ViewData["motorista_id"] = new SelectList(_context.motorista.Where(d => d.Situacao == "1"), "Id", "Nome");
            return View();
        }

        // POST: Cadastro/Usuario_Motorista/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Create([Bind("Id,usuario_id,motorista_id,DataInicial,DataFinal,Observacao")] usuario_motorista usuario_motorista)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario_motorista);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["motorista_id"] = new SelectList(_context.motorista.Where(d => d.Situacao == "1"), "Id", "Nome", usuario_motorista.motorista_id);
            ViewData["usuario_id"] = new SelectList(_context.usuario, "Id", "Sus", usuario_motorista.usuario_id);
            return View(usuario_motorista);
        }

        // GET: Cadastro/Usuario_Motorista/Edit/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario_motorista = await _context.usuario_motorista.FindAsync(id);
            if (usuario_motorista == null)
            {
                return NotFound();
            }
            var usuarios = _context.usuario.Where(u => u.Geral.Tipo == "2" && u.Geral.Situacao == "1" )
                .Select(f => new
                {
                    Id = f.Id,
                    Nome = f.Geral.Nome
                }).ToList();
            ViewData["usuario_id"] = new SelectList(usuarios, "Id", "Nome");
            ViewData["motorista_id"] = new SelectList(_context.motorista.Where(d => d.Situacao == "1"), "Id", "Nome");
            return View(usuario_motorista);
        }

        // POST: Cadastro/Usuario_Motorista/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Edit(int id, [Bind("Id,usuario_id,motorista_id,DataInicial,DataFinal,Observacao")] usuario_motorista usuario_motorista)
        {
            if (id != usuario_motorista.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario_motorista);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!usuario_motoristaExists(usuario_motorista.Id))
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
            ViewData["motorista_id"] = new SelectList(_context.motorista, "Id", "Nome", usuario_motorista.motorista_id);
            ViewData["usuario_id"] = new SelectList(_context.usuario, "Id", "Sus", usuario_motorista.usuario_id);
            return View(usuario_motorista);
        }

        // GET: Cadastro/Usuario_Motorista/Delete/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario_motorista = await _context.usuario_motorista
                .Include(u => u.Motorista)
                .Include(u => u.Usuario)
                .ThenInclude(D => D.Geral)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario_motorista == null)
            {
                return NotFound();
            }

            return View(usuario_motorista);
        }

        // POST: Cadastro/Usuario_Motorista/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario_motorista = await _context.usuario_motorista.FindAsync(id);
            _context.usuario_motorista.Remove(usuario_motorista);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool usuario_motoristaExists(int id)
        {
            return _context.usuario_motorista.Any(e => e.Id == id);
        }
    }
}
