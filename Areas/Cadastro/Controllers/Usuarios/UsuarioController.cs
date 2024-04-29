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
    public class UsuarioController : Controller
    {
        private readonly ApaDbContext _context;

        public UsuarioController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/Usuario
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Index()
        {
            var apaDbContext = _context.usuario.Include(u => u.Beneficio).Include(u => u.Comorbidade).Include(u => u.Escola).Include(u => u.Geral).Include(u => u.MotivoDesligamento);
            return View(await apaDbContext.ToListAsync());
        }

        // GET: Cadastro/Usuario/Details/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.usuario
                .Include(u => u.Beneficio)
                .Include(u => u.Comorbidade)
                .Include(u => u.Escola)
                .Include(u => u.Geral)
                .Include(u => u.MotivoDesligamento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Cadastro/Usuario/Create
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public IActionResult Create()
        {
            ViewData["beneficio_id"] = new SelectList(_context.beneficio, "Id", "Nome");
            ViewData["comorbidade_id"] = new SelectList(_context.comorbidade, "Id", "Descricao");
            ViewData["escolas_id"] = new SelectList(_context.escolas, "Id", "Cidade");
            ViewData["geral_id"] = new SelectList(_context.geral, "Id", "Cep");
            ViewData["desligamento_id"] = new SelectList(_context.desligamento_motivos_usuario, "Id", "Motivo");
            return View();
        }

        // POST: Cadastro/Usuario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Create([Bind("Id,geral_id,Situacao,Foto,Sus,Nascimento,Ingresso,DataLaudo,escolas_id,Serie,beneficio_id,comorbidade_id,HistoricoContato,desligamento_id,DataDesligamento,usuario_descritivo_desligamento,Alergia,Medicacao,RestricaoAlimentar,Transporte")] usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["beneficio_id"] = new SelectList(_context.beneficio, "Id", "Nome", usuario.beneficio_id);
            ViewData["comorbidade_id"] = new SelectList(_context.comorbidade, "Id", "Descricao", usuario.comorbidade_id);
            ViewData["escolas_id"] = new SelectList(_context.escolas, "Id", "Cidade", usuario.escolas_id);
            ViewData["geral_id"] = new SelectList(_context.geral, "Id", "Cep", usuario.geral_id);
            ViewData["desligamento_id"] = new SelectList(_context.desligamento_motivos_usuario, "Id", "Motivo", usuario.desligamento_id);
            return View(usuario);
        }

        // GET: Cadastro/Usuario/Edit/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["beneficio_id"] = new SelectList(_context.beneficio, "Id", "Nome", usuario.beneficio_id);
            ViewData["comorbidade_id"] = new SelectList(_context.comorbidade, "Id", "Descricao", usuario.comorbidade_id);
            ViewData["escolas_id"] = new SelectList(_context.escolas, "Id", "Cidade", usuario.escolas_id);
            ViewData["geral_id"] = new SelectList(_context.geral, "Id", "Cep", usuario.geral_id);
            ViewData["desligamento_id"] = new SelectList(_context.desligamento_motivos_usuario, "Id", "Motivo", usuario.desligamento_id);
            return View(usuario);
        }

        // POST: Cadastro/Usuario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Edit(int id, [Bind("Id,geral_id,Situacao,Foto,Sus,Nascimento,Ingresso,DataLaudo,escolas_id,Serie,beneficio_id,comorbidade_id,HistoricoContato,desligamento_id,DataDesligamento,usuario_descritivo_desligamento,Alergia,Medicacao,RestricaoAlimentar,Transporte")] usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!usuarioExists(usuario.Id))
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
            ViewData["beneficio_id"] = new SelectList(_context.beneficio, "Id", "Nome", usuario.beneficio_id);
            ViewData["comorbidade_id"] = new SelectList(_context.comorbidade, "Id", "Descricao", usuario.comorbidade_id);
            ViewData["escolas_id"] = new SelectList(_context.escolas, "Id", "Cidade", usuario.escolas_id);
            ViewData["geral_id"] = new SelectList(_context.geral, "Id", "Cep", usuario.geral_id);
            ViewData["desligamento_id"] = new SelectList(_context.desligamento_motivos_usuario, "Id", "Motivo", usuario.desligamento_id);
            return View(usuario);
        }

        // GET: Cadastro/Usuario/Delete/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.usuario
                .Include(u => u.Beneficio)
                .Include(u => u.Comorbidade)
                .Include(u => u.Escola)
                .Include(u => u.Geral)
                .Include(u => u.MotivoDesligamento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Cadastro/Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.usuario.FindAsync(id);
            _context.usuario.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool usuarioExists(int id)
        {
            return _context.usuario.Any(e => e.Id == id);
        }
    }
}
