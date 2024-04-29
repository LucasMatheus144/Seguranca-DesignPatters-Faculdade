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
    public class UsuarioSalaController : Controller
    {
        private readonly ApaDbContext _context;

        public UsuarioSalaController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/UsuarioSala
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> Index()
        {
            var apaDbContext = _context.usuario_sala.Include(u => u.Geral).Include(u => u.Sala).Include(u => u.Usuario);
            return View(await apaDbContext.ToListAsync());
        }

        // GET: Cadastro/UsuarioSala/Details/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario_sala = await _context.usuario_sala
                .Include(u => u.Geral)
                .Include(u => u.Sala)
                .Include(u => u.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario_sala == null)
            {
                return NotFound();
            }

            return View(usuario_sala);
        }

        // GET: Cadastro/UsuarioSala/Create
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
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
            return View();
        }

        // POST: Cadastro/UsuarioSala/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       [HttpPost]
[ValidateAntiForgeryToken]
[Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
public async Task<IActionResult> Create([Bind("Id,geral_id,usuario_id,sala_id,Periodo,Inicio,Final")] usuario_sala usuario_sala)
{
    try
    {
        // Pegar o Id geral
        usuario_sala.geral_id = _context.usuario.Where(u => u.Id == usuario_sala.usuario_id).Select(u => u.geral_id).FirstOrDefault();

        if (ModelState.IsValid)
        {
            _context.Add(usuario_sala);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
    catch (DbUpdateException)
    {
        TempData["ErrorMessage"] = "Falha no cadastro, por gentileza verifique se o usuário já está cadastrado em uma sala.";
        return RedirectToAction(nameof(Index));
    }

    ViewData["sala_id"] = new SelectList(_context.sala, "Id", "Nome");
     var usuarios = _context.usuario.Where(u => u.Geral.Tipo == "2" && u.Geral.Situacao == "1" )
            .Select(f => new
            {
                Id = f.Id,
                Nome = f.Geral.Nome
            }).ToList();
            ViewData["usuarios"] = new SelectList(usuarios, "Id", "Nome");
    return View(usuario_sala);
}


        // GET: Cadastro/UsuarioSala/Edit/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario_sala = await _context.usuario_sala.FindAsync(id);
            if (usuario_sala == null)
            {
                return NotFound();
            }
            ViewData["sala_id"] = new SelectList(_context.sala, "Id", "Nome", usuario_sala.sala_id);
             var usuarios = _context.usuario.Where(u => u.Geral.Tipo == "2" && u.Geral.Situacao == "1" )
            .Select(f => new
            {
                Id = f.Id,
                Nome = f.Geral.Nome
            }).ToList();
            ViewData["usuarios"] = new SelectList(usuarios, "Id", "Nome");
            return View(usuario_sala);
        }

        // POST: Cadastro/UsuarioSala/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> Edit(int id, [Bind("Id,geral_id,usuario_id,sala_id,Periodo,Inicio,Final")] usuario_sala usuario_sala)
        {
            if (id != usuario_sala.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                     // Pegar o Id geral
                    usuario_sala.geral_id = _context.usuario.Where(u => u.Id == usuario_sala.usuario_id).Select(u => u.geral_id).FirstOrDefault();
                    _context.Update(usuario_sala);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!usuario_salaExists(usuario_sala.Id))
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
            ViewData["sala_id"] = new SelectList(_context.sala, "Id", "Nome", usuario_sala.sala_id);
             var usuarios = _context.usuario.Where(u => u.Geral.Tipo == "2" && u.Geral.Situacao == "1" )
            .Select(f => new
            {
                Id = f.Id,
                Nome = f.Geral.Nome
            }).ToList();
            ViewData["usuarios"] = new SelectList(usuarios, "Id", "Nome");
            return View(usuario_sala);
        }

        // GET: Cadastro/UsuarioSala/Delete/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario_sala = await _context.usuario_sala
                .Include(u => u.Geral)
                .Include(u => u.Sala)
                .Include(u => u.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario_sala == null)
            {
                return NotFound();
            }

            return View(usuario_sala);
        }

        // POST: Cadastro/UsuarioSala/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario_sala = await _context.usuario_sala.FindAsync(id);
            _context.usuario_sala.Remove(usuario_sala);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool usuario_salaExists(int id)
        {
            return _context.usuario_sala.Any(e => e.Id == id);
        }
    }
}
