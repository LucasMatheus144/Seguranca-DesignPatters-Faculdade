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
    public class CTAReceberController : Controller
    {
        private readonly ApaDbContext _context;

        public CTAReceberController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/CTAReceber
        public async Task<IActionResult> Index()
        {
            return View(await _context.cta_receber.ToListAsync());
        }

        // GET: Cadastro/CTAReceber/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cta_receber = await _context.cta_receber
                .FirstOrDefaultAsync(m => m.ContaReceberId == id);
            if (cta_receber == null)
            {
                return NotFound();
            }

            return View(cta_receber);
        }

        // GET: Cadastro/CTAReceber/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cadastro/CTAReceber/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContaReceberId,MovimentoId,MovimentoTipo,MovimentoParcelas,CtaPagamento,CtaValor,CtaMulta,CtaJuros,CtaReceberBaixa")] cta_receber cta_receber)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cta_receber);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cta_receber);
        }

        // GET: Cadastro/CTAReceber/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cta_receber = await _context.cta_receber.FindAsync(id);
            if (cta_receber == null)
            {
                return NotFound();
            }
            return View(cta_receber);
        }

        // POST: Cadastro/CTAReceber/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContaReceberId,MovimentoId,MovimentoTipo,MovimentoParcelas,CtaPagamento,CtaValor,CtaMulta,CtaJuros,CtaReceberBaixa")] cta_receber cta_receber)
        {
            if (id != cta_receber.ContaReceberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cta_receber);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!cta_receberExists(cta_receber.ContaReceberId))
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
            return View(cta_receber);
        }

        // GET: Cadastro/CTAReceber/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cta_receber = await _context.cta_receber
                .FirstOrDefaultAsync(m => m.ContaReceberId == id);
            if (cta_receber == null)
            {
                return NotFound();
            }

            return View(cta_receber);
        }

        // POST: Cadastro/CTAReceber/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cta_receber = await _context.cta_receber.FindAsync(id);
            _context.cta_receber.Remove(cta_receber);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool cta_receberExists(int id)
        {
            return _context.cta_receber.Any(e => e.ContaReceberId == id);
        }
    }
}
