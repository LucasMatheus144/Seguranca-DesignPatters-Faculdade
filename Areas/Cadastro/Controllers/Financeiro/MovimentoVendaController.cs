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
    public class MovimentoVendaController : Controller
    {
        private readonly ApaDbContext _context;

        public MovimentoVendaController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/MovimentoVenda
        public async Task<IActionResult> Index()
        {
            var apaDbContext = _context.movimento_venda.Include(m => m.Geral);
            return View(await apaDbContext.ToListAsync());
        }

        // GET: Cadastro/MovimentoVenda/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimento_venda = await _context.movimento_venda
                .Include(m => m.Geral)
                .FirstOrDefaultAsync(m => m.movimento_id == id);
            if (movimento_venda == null)
            {
                return NotFound();
            }

            return View(movimento_venda);
        }

        // GET: Cadastro/MovimentoVenda/Create
      public IActionResult Create()
        {
            var geralIds = _context.geral
                .Where(g => g.Tipo != "2") 
                .Select(g => new { g.Id, g.Nome })
                .ToList();
            
            ViewData["geral_id"] = new SelectList(geralIds, "Id", "Nome");
            
            return View();
        }

        // POST: Cadastro/MovimentoVenda/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("movimento_id,geral_id,geral_tipo,movimento_tipo,movimento_outro,movimento_parcelas,movimento_dia_vencto,movimento_data,movimento_valor,movimento_tipo_pagto,movimento_email,movimento_data_estorno")] movimento_venda movimento_venda)
        {
            var situacao = await _context.geral.Where(g => g.Id == movimento_venda.geral_id).Select(g => g.Situacao).FirstOrDefaultAsync();
            movimento_venda.geral_tipo = situacao.ToString();
            if (ModelState.IsValid)
            {
                _context.Add(movimento_venda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["geral_id"] = new SelectList(_context.geral, "Id", "Nome", movimento_venda.geral_id);
            return View(movimento_venda);
        }

        // GET: Cadastro/MovimentoVenda/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimento_venda = await _context.movimento_venda.FindAsync(id);
            if (movimento_venda == null)
            {
                return NotFound();
            }
            ViewData["geral_id"] = new SelectList(_context.geral, "Id", "Nome", movimento_venda.geral_id);
            return View(movimento_venda);
        }

        // POST: Cadastro/MovimentoVenda/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("movimento_id,geral_id,geral_tipo,movimento_tipo,movimento_outro,movimento_parcelas,movimento_dia_vencto,movimento_data,movimento_valor,movimento_tipo_pagto,movimento_email,movimento_data_estorno")] movimento_venda movimento_venda)
        {
            if (id != movimento_venda.movimento_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movimento_venda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!movimento_vendaExists(movimento_venda.movimento_id))
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
            ViewData["geral_id"] = new SelectList(_context.geral, "Id", "Cep", movimento_venda.geral_id);
            return View(movimento_venda);
        }

        // GET: Cadastro/MovimentoVenda/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimento_venda = await _context.movimento_venda
                .Include(m => m.Geral)
                .FirstOrDefaultAsync(m => m.movimento_id == id);
            if (movimento_venda == null)
            {
                return NotFound();
            }

            return View(movimento_venda);
        }

        // POST: Cadastro/MovimentoVenda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movimento_venda = await _context.movimento_venda.FindAsync(id);
            _context.movimento_venda.Remove(movimento_venda);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool movimento_vendaExists(int id)
        {
            return _context.movimento_venda.Any(e => e.movimento_id == id);
        }
    }
}
