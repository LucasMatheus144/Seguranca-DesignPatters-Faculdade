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
    public class ProdutosController : Controller
    {
        private readonly ApaDbContext _context;

        public ProdutosController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/Produtos
        public async Task<IActionResult> Index()
        {
            return View(await _context.produtos.ToListAsync());
        }

        // GET: Cadastro/Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtos = await _context.produtos
                .FirstOrDefaultAsync(m => m.produtos_id == id);
            if (produtos == null)
            {
                return NotFound();
            }

            return View(produtos);
        }

        // GET: Cadastro/Produtos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cadastro/Produtos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("produtos_id,produtos_descricao,produtos_unidade,produtos_qtde_unit,produtos_evento")] produtos produtos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produtos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produtos);
        }

        // GET: Cadastro/Produtos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtos = await _context.produtos.FindAsync(id);
            if (produtos == null)
            {
                return NotFound();
            }
            return View(produtos);
        }

        // POST: Cadastro/Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("produtos_id,produtos_descricao,produtos_unidade,produtos_qtde_unit,produtos_evento")] produtos produtos)
        {
            if (id != produtos.produtos_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produtos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!produtosExists(produtos.produtos_id))
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
            return View(produtos);
        }

        // GET: Cadastro/Produtos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtos = await _context.produtos
                .FirstOrDefaultAsync(m => m.produtos_id == id);
            if (produtos == null)
            {
                return NotFound();
            }

            return View(produtos);
        }

        // POST: Cadastro/Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produtos = await _context.produtos.FindAsync(id);
            _context.produtos.Remove(produtos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool produtosExists(int id)
        {
            return _context.produtos.Any(e => e.produtos_id == id);
        }
    }
}
