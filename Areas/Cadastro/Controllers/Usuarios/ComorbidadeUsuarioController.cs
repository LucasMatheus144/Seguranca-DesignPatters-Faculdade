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
    [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
    public class ComorbidadeUsuarioController : Controller
    {
        private readonly ApaDbContext _context;

        public ComorbidadeUsuarioController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/ComorbidadeUsuario
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> Index()
        {
            var apaDbContext = _context.comorbidade_usuario.Include(c => c.Comorbidade).Include(c => c.Usuario).ThenInclude(d => d.Geral);
            return View(await apaDbContext.ToListAsync());
        }

        // GET: Cadastro/ComorbidadeUsuario/Details/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comorbidade_usuario = await _context.comorbidade_usuario
                .Include(c => c.Comorbidade)
                .Include(c => c.Usuario)
                .ThenInclude(d => d.Geral)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comorbidade_usuario == null)
            {
                return NotFound();
            }

            return View(comorbidade_usuario);
        }

        // GET: Cadastro/ComorbidadeUsuario/Create
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public IActionResult Create()
        {
            var usuarios = _context.usuario.Where(u => u.Geral.Situacao == "1" )
            .Select(f => new
            {
                Id = f.Id,
                Nome = f.Geral.Nome
            }).ToList();
            ViewData["usuario_id"] = new SelectList(usuarios, "Id", "Nome");
            ViewData["comorbidade_id"] = new SelectList(_context.comorbidade, "Id", "Descricao");

            return View();
        }

        // POST: Cadastro/ComorbidadeUsuario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser, TipoUsuario.Admin, TipoUsuario.Funcionarios })]
        public async Task<IActionResult> Create([Bind("Id,usuario_id,comorbidade_id")] comorbidade_usuario comorbidade_usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var duplicidadeComorbidade = await _context.comorbidade_usuario.FirstOrDefaultAsync(cu => cu.usuario_id == comorbidade_usuario.usuario_id);

                    if (duplicidadeComorbidade != null)
                    {
                        if (duplicidadeComorbidade.comorbidade_id == comorbidade_usuario.comorbidade_id)
                        {
                            ModelState.AddModelError(string.Empty, "Essa comorbidade já está registrada para o usuário selecionado.");
                            return View(comorbidade_usuario);
                        }
                    }

                    _context.Add(comorbidade_usuario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro ao salvar os dados. Por favor, tente novamente mais tarde.";
                return RedirectToAction(nameof(Index));
            }

            ViewData["comorbidade_id"] = new SelectList(_context.comorbidade, "Id", "Descricao", comorbidade_usuario.comorbidade_id);
            var usuarios = _context.usuario.Where(u => u.Geral.Situacao == "1")
                                            .Select(f => new
                                            {
                                                Id = f.Id,
                                                Nome = f.Geral.Nome
                                            }).ToList();
            ViewData["usuario_id"] = new SelectList(usuarios, "Id", "Nome");
            return View(comorbidade_usuario);
        }


        // GET: Cadastro/ComorbidadeUsuario/Edit/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comorbidade_usuario = await _context.comorbidade_usuario.FindAsync(id);
            if (comorbidade_usuario == null)
            {
                return NotFound();
            }
            ViewData["comorbidade_id"] = new SelectList(_context.comorbidade, "Id", "Descricao", comorbidade_usuario.comorbidade_id);
             var usuarios = _context.usuario.Where(u => u.Geral.Situacao == "1" )
            .Select(f => new
            {
                Id = f.Id,
                Nome = f.Geral.Nome
            }).ToList();
            ViewData["usuario_id"] = new SelectList(usuarios, "Id", "Nome");
            return View(comorbidade_usuario);
        }

        // POST: Cadastro/ComorbidadeUsuario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> Edit(int id, [Bind("Id,usuario_id,comorbidade_id")] comorbidade_usuario comorbidade_usuario)
        {
            if (id != comorbidade_usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comorbidade_usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!comorbidade_usuarioExists(comorbidade_usuario.Id))
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
            ViewData["comorbidade_id"] = new SelectList(_context.comorbidade, "Id", "Descricao", comorbidade_usuario.comorbidade_id);
            ViewData["usuario_id"] = new SelectList(_context.usuario, "Id", "Sus", comorbidade_usuario.usuario_id);
            return View(comorbidade_usuario);
        }

        // GET: Cadastro/ComorbidadeUsuario/Delete/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comorbidade_usuario = await _context.comorbidade_usuario
                .Include(c => c.Comorbidade)
                .Include(c => c.Usuario)
                .ThenInclude(d => d.Geral)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comorbidade_usuario == null)
            {
                return NotFound();
            }

            return View(comorbidade_usuario);
        }

        // POST: Cadastro/ComorbidadeUsuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comorbidade_usuario = await _context.comorbidade_usuario.FindAsync(id);
            _context.comorbidade_usuario.Remove(comorbidade_usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool comorbidade_usuarioExists(int id)
        {
            return _context.comorbidade_usuario.Any(e => e.Id == id);
        }
    }
}
