using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EspacoPotencial.Areas.Cadastro.Models.Financeiro;
using EspacoPotencial.Context;
using EspacoPotencial.Filters;
using EspacoPotencial.Models.Account;

namespace EspacoPotencial.Areas.Cadastro.Controllers.Financeiro
{
    [Area("Cadastro")]
    [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
    public class ProdutosEventosController : Controller
    {
        private readonly ApaDbContext _context;

        public ProdutosEventosController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/ProdutosEventos
        public async Task<IActionResult> Index()
        {
            var apaDbContext = _context.produtos_evento.Include(p => p.Evento).Include(p => p.GrupoPermitido).Include(p => p.Produto);
            return View(await apaDbContext.ToListAsync());
        }

        // GET: Cadastro/ProdutosEventos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtos_evento = await _context.produtos_evento
                .Include(p => p.Evento)
                .Include(p => p.GrupoPermitido)
                .Include(p => p.Produto)
                .FirstOrDefaultAsync(m => m.prod_evento_id == id);
            if (produtos_evento == null)
            {
                return NotFound();
            }

            return View(produtos_evento);
        }

        // GET: Cadastro/ProdutosEventos/Create
        public IActionResult Create()
        {
            ViewData["eventos_id"] = new SelectList(_context.eventos, "eventos_id", "evento_nome");
            ViewData["grp_id"] = new SelectList(_context.grupo_permitido, "grp_id", "grp_descricao");
            var produtosComEvento = _context.produtos.Where(p => p.produtos_evento == "S");
            ViewData["produtos_id"] = new SelectList(produtosComEvento, "produtos_id", "produtos_descricao");
            return View();
        }

        // POST: Cadastro/ProdutosEventos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("prod_evento_id,eventos_id,grp_id,produtos_id,prod_evento_valor_uni")] produtos_evento produtos_evento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produtos_evento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["eventos_id"] = new SelectList(_context.eventos, "eventos_id", "evento_nome", produtos_evento.eventos_id);
            ViewData["grp_id"] = new SelectList(_context.grupo_permitido, "grp_id", "grp_descricao", produtos_evento.grp_id);
            ViewData["produtos_id"] = new SelectList(_context.produtos, "produtos_id", "produtos_descricao");

            return View(produtos_evento);
        }

        // GET: Cadastro/ProdutosEventos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtos_evento = await _context.produtos_evento.FindAsync(id);
            if (produtos_evento == null)
            {
                return NotFound();
            }
            ViewData["eventos_id"] = new SelectList(_context.eventos, "eventos_id", "evento_nome", produtos_evento.eventos_id);
            ViewData["grp_id"] = new SelectList(_context.grupo_permitido, "grp_id", "grp_descricao", produtos_evento.grp_id);
            ViewData["produtos_id"] = new SelectList(_context.produtos, "produtos_id", "produtos_descricao");

            return View(produtos_evento);
        }

        // POST: Cadastro/ProdutosEventos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("prod_evento_id,eventos_id,grp_id,produtos_id,prod_evento_valor_uni")] produtos_evento produtos_evento)
        {
            if (id != produtos_evento.prod_evento_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produtos_evento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!produtos_eventoExists(produtos_evento.prod_evento_id))
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
            ViewData["eventos_id"] = new SelectList(_context.eventos, "eventos_id", "evento_nome", produtos_evento.eventos_id);
            ViewData["grp_id"] = new SelectList(_context.grupo_permitido, "grp_id", "grp_descricao", produtos_evento.grp_id);
            ViewData["produtos_id"] = new SelectList(_context.produtos, "produtos_id", "produtos_descricao");

            return View(produtos_evento);
        }

        // GET: Cadastro/ProdutosEventos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtos_evento = await _context.produtos_evento
                .Include(p => p.Evento)
                .Include(p => p.GrupoPermitido)
                .Include(p => p.Produto)
                .FirstOrDefaultAsync(m => m.prod_evento_id == id);
            if (produtos_evento == null)
            {
                return NotFound();
            }

            return View(produtos_evento);
        }

        // POST: Cadastro/ProdutosEventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produtos_evento = await _context.produtos_evento.FindAsync(id);
            _context.produtos_evento.Remove(produtos_evento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool produtos_eventoExists(int id)
        {
            return _context.produtos_evento.Any(e => e.prod_evento_id == id);
        }
    }
}
