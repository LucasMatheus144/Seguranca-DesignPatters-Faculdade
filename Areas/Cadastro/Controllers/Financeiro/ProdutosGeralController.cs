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
    public class ProdutosGeralController : Controller
    {
        private readonly ApaDbContext _context;

        public ProdutosGeralController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/ProdutosGeral
        public async Task<IActionResult> Index()
        {
            var apaDbContext = _context.produtos_geral.Include(p => p.GrupoPermitido).Include(p => p.Produto);
            return View(await apaDbContext.ToListAsync());
        }

        // GET: Cadastro/ProdutosGeral/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtos_geral = await _context.produtos_geral
                .Include(p => p.GrupoPermitido)
                .Include(p => p.Produto)
                .FirstOrDefaultAsync(m => m.produto_geral_id == id);
            if (produtos_geral == null)
            {
                return NotFound();
            }

            return View(produtos_geral);
        }

        // GET: Cadastro/ProdutosGeral/Create
        public IActionResult Create()
        {
            ViewData["grp_id"] = new SelectList(_context.grupo_permitido, "grp_id", "grp_descricao");
            ViewData["produto_id"] = new SelectList(_context.produtos, "produtos_id", "produtos_descricao");
            return View();
        }

        // POST: Cadastro/ProdutosGeral/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("produto_geral_id,grp_id,produto_id,produto_geral_valor_uni")] produtos_geral produtos_geral)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produtos_geral);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["grp_id"] = new SelectList(_context.grupo_permitido, "grp_id", "grp_descricao", produtos_geral.grp_id);
            ViewData["produto_id"] = new SelectList(_context.produtos, "produtos_id", "produtos_descricao", produtos_geral.produto_id);
            return View(produtos_geral);
        }

        // GET: Cadastro/ProdutosGeral/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtos_geral = await _context.produtos_geral.FindAsync(id);
            if (produtos_geral == null)
            {
                return NotFound();
            }
            ViewData["grp_id"] = new SelectList(_context.grupo_permitido, "grp_id", "grp_descricao", produtos_geral.grp_id);
            ViewData["produto_id"] = new SelectList(_context.produtos, "produtos_id", "produtos_descricao", produtos_geral.produto_id);
            return View(produtos_geral);
        }

        // POST: Cadastro/ProdutosGeral/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("produto_geral_id,grp_id,produto_id,produto_geral_valor_uni")] produtos_geral produtos_geral)
        {
            if (id != produtos_geral.produto_geral_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produtos_geral);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!produtos_geralExists(produtos_geral.produto_geral_id))
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
            ViewData["grp_id"] = new SelectList(_context.grupo_permitido, "grp_id", "grp_descricao", produtos_geral.grp_id);
            ViewData["produto_id"] = new SelectList(_context.produtos, "produtos_id", "produtos_descricao", produtos_geral.produto_id);
            return View(produtos_geral);
        }

        // GET: Cadastro/ProdutosGeral/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtos_geral = await _context.produtos_geral
                .Include(p => p.GrupoPermitido)
                .Include(p => p.Produto)
                .FirstOrDefaultAsync(m => m.produto_geral_id == id);
            if (produtos_geral == null)
            {
                return NotFound();
            }

            return View(produtos_geral);
        }

        // POST: Cadastro/ProdutosGeral/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produtos_geral = await _context.produtos_geral.FindAsync(id);
            _context.produtos_geral.Remove(produtos_geral);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool produtos_geralExists(int id)
        {
            return _context.produtos_geral.Any(e => e.produto_geral_id == id);
        }
    }
}
