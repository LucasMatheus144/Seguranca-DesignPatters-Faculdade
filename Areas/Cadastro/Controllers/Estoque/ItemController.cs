using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EspacoPotencial.Areas.Cadastro.Models.Financeiro;
using EspacoPotencial.Context;
using EspacoPotencial.master.Areas.Cadastro.Models.Estoque;

namespace EspacoPotencial.Areas.Cadastro.Controllers.Estoque
{
    [Area("Cadastro")]
    public class ItemController : Controller
    {
        private readonly ApaDbContext _context;

        public ItemController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/Item
        public async Task<IActionResult> Index()
        {
            var apaDbContext = _context.item.Include(i => i.armazem).Include(i => i.tipo);
            return View(await apaDbContext.ToListAsync());
        }

        // GET: Cadastro/Item/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.item
                .Include(i => i.armazem)
                .Include(i => i.tipo)
                .FirstOrDefaultAsync(m => m.id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Cadastro/Item/Create
        public IActionResult Create()
        {
            ViewData["estoque_id"] = new SelectList(_context.armazem, "id", "nome");
            ViewData["tipo_armazem"] = new SelectList(_context.tipo, "id", "descricao");
            return View();
        }

        // POST: Cadastro/Item/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,estoque_id,descricao,qntdeunitario,valorunitario,situacao,tipo_armazem")] item item)
        {
            if (ModelState.IsValid)
            {
                if (!await item.ValidateItemAsync(_context))
                {
                    ModelState.AddModelError(string.Empty, "O tipo de armazém do item é diferente do tipo de armazém do estoque.");
                    ViewData["estoque_id"] = new SelectList(_context.armazem, "id", "nome", item.estoque_id);
                    ViewData["tipo_armazem"] = new SelectList(_context.tipo, "id", "descricao", item.tipo_armazem);
                    return View(item);
                }

                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["estoque_id"] = new SelectList(_context.armazem, "id", "nome", item.estoque_id);
            ViewData["tipo_armazem"] = new SelectList(_context.tipo, "id", "descricao", item.tipo_armazem);
            return View(item);
        }
      

        // GET: Cadastro/Item/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.item.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            if (item.situacao == "3")
            {
                TempData["Message"] = "Não é possivel editar um item que esteja pendente.";
                return RedirectToAction(nameof(Index)); 
            }
            ViewData["estoque_id"] = new SelectList(_context.armazem, "id", "nome", item.estoque_id);
            ViewData["tipo_armazem"] = new SelectList(_context.tipo, "id", "descricao", item.tipo_armazem);
            return View(item);
        }

        // POST: Cadastro/Item/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,estoque_id,descricao,qntdeunitario,valorunitario,situacao,tipo_armazem")] item item)
        {
            if (id != item.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var validator = new ValidItemState();
                if (!await validator.ValidateItemAsync(item, _context))
                {
                    ModelState.AddModelError(string.Empty, "O tipo de armazém do item é diferente do tipo de armazém do armazém.");
                    ViewData["estoque_id"] = new SelectList(_context.armazem, "id", "nome", item.estoque_id);
                    ViewData["tipo_armazem"] = new SelectList(_context.tipo, "id", "descricao", item.tipo_armazem);
                    return View(item);
                }

               bool isValid = await item.ValidateItemAsync(_context);

                if (isValid)
                {
                    try
                    {
                        _context.Update(item);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!itemExists(item.id))
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
                else
                {
                    ModelState.AddModelError(string.Empty, "O item não passou na validação.");
                    ViewData["estoque_id"] = new SelectList(_context.armazem, "id", "nome", item.estoque_id);
                    ViewData["tipo_armazem"] = new SelectList(_context.tipo, "id", "descricao", item.tipo_armazem);
                    return View(item);
        }
            }
            ViewData["estoque_id"] = new SelectList(_context.armazem, "id", "nome", item.estoque_id);
            ViewData["tipo_armazem"] = new SelectList(_context.tipo, "id", "descricao", item.tipo_armazem);
            return View(item);
        }


        // GET: Cadastro/Item/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.item
                .Include(i => i.armazem)
                .Include(i => i.tipo)
                .FirstOrDefaultAsync(m => m.id == id);
            if (item == null)
            {
                return NotFound();
            }
            if (item.situacao == "3")
            {
                TempData["Message"] = "Não é possivel excluir um item que esteja pendente.";
                return RedirectToAction(nameof(Index)); 
            }

            return View(item);
        }

        // POST: Cadastro/Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.item.FindAsync(id);
            _context.item.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool itemExists(int id)
        {
            return _context.item.Any(e => e.id == id);
        }
    }
}
