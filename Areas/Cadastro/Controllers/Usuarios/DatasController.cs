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
    public class DatasController : Controller
    {
        private readonly ApaDbContext _context;

        public DatasController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/Datas
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Index()
        {
            return View(await _context.datas.ToListAsync());
        }

        // GET: Cadastro/Datas/Details/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datas = await _context.datas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (datas == null)
            {
                return NotFound();
            }

            return View(datas);
        }

        // GET: Cadastro/Datas/Create
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cadastro/Datas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Create([Bind("Id,Ano,Tipo,Descritivo,DataInicio,DataFinal")] datas datas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(datas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(datas);
        }

        // GET: Cadastro/Datas/Edit/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datas = await _context.datas.FindAsync(id);
            if (datas == null)
            {
                return NotFound();
            }
            return View(datas);
        }

        // POST: Cadastro/Datas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ano,Tipo,Descritivo,DataInicio,DataFinal")] datas datas)
        {
            if (id != datas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(datas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!datasExists(datas.Id))
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
            return View(datas);
        }

        // GET: Cadastro/Datas/Delete/5
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datas = await _context.datas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (datas == null)
            {
                return NotFound();
            }

            return View(datas);
        }

        // POST: Cadastro/Datas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var datas = await _context.datas.FindAsync(id);
            _context.datas.Remove(datas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool datasExists(int id)
        {
            return _context.datas.Any(e => e.Id == id);
        }
    }
}
