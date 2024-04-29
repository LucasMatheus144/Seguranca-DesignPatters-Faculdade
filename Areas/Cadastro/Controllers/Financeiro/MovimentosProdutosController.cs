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
    public class MovimentosProdutosController : Controller
    {
        private readonly ApaDbContext _context;

        public MovimentosProdutosController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/MovimentosProdutos
        public async Task<IActionResult> Index()
        {
            return View(await _context.movimento_produtos.ToListAsync());
        }

        // GET: Cadastro/MovimentosProdutos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimento_produtos = await _context.movimento_produtos
                
                .FirstOrDefaultAsync(m => m.movimento_id == id);
            if (movimento_produtos == null)
            {
                return NotFound();
            }

            return View(movimento_produtos);
        }

        // GET: Cadastro/MovimentosProdutos/Create
        public IActionResult Create()
        {
            ViewData["produto_geral"] = new SelectList(_context.produtos_geral, "produto_geral_id", "produto_id");
            ViewData["produto_evento"] = new SelectList(_context.produtos_evento, "produto_evento_id", "eventos_id");

            return View();
        }

        // POST: Cadastro/MovimentosProdutos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovimentoId,EventoId,ProdutoGeralId,ProdutoEventoId,ProdutosQuantidade,ProdutosValorUnitario,ProdutosValorTotal")] movimento_produtos movimento_produtos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movimento_produtos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movimento_produtos);
        }

        // GET: Cadastro/MovimentosProdutos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimento_produtos = await _context.movimento_produtos.FindAsync(id);
            if (movimento_produtos == null)
            {
                return NotFound();
            }
            return View(movimento_produtos);
        }

        // POST: Cadastro/MovimentosProdutos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovimentoId,EventoId,ProdutoGeralId,ProdutoEventoId,ProdutosQuantidade,ProdutosValorUnitario,ProdutosValorTotal")] movimento_produtos movimento_produtos)
        {
            if (id != movimento_produtos.movimento_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movimento_produtos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!movimento_produtosExists(movimento_produtos.movimento_id))
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
            return View(movimento_produtos);
        }

        // GET: Cadastro/MovimentosProdutos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimento_produtos = await _context.movimento_produtos
                .FirstOrDefaultAsync(m => m.movimento_id == id);
            if (movimento_produtos == null)
            {
                return NotFound();
            }

            return View(movimento_produtos);
        }

        // POST: Cadastro/MovimentosProdutos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movimento_produtos = await _context.movimento_produtos.FindAsync(id);
            _context.movimento_produtos.Remove(movimento_produtos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool movimento_produtosExists(int id)
        {
            return _context.movimento_produtos.Any(e => e.movimento_id == id);
        }
    }
}
