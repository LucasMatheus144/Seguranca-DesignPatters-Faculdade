using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EspacoPotencial.Areas.Cadastro.Models.Financeiro;
using EspacoPotencial.Context;

namespace EspacoPotencial.Areas.Cadastro.Controllers.Estoque
{
    [Area("Cadastro")]
    public class MovimentacaoItemController : Controller
    {
        private readonly ApaDbContext _context;

        public MovimentacaoItemController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/MovimentacaoItem
        public async Task<IActionResult> Index()
        {
            var apaDbContext = _context.movimentacao.Include(m => m.item).Include(m => m.usuario).ThenInclude(d => d.Geral);
            return View(await apaDbContext.ToListAsync());
        }

        // GET: Cadastro/MovimentacaoItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentacao = await _context.movimentacao
                .Include(m => m.item)
                .Include(m => m.usuario)
                .ThenInclude(d => d.Geral)
                .FirstOrDefaultAsync(m => m.id == id);
            if (movimentacao == null)
            {
                return NotFound();
            }

            return View(movimentacao);
        }

        // GET: Cadastro/MovimentacaoItem/Create
        public IActionResult Create()
        {
           
            var usuarios = _context.usuario.Where(u => u.Geral.Situacao == "1" )
            .Select(f => new
            {
                Id = f.Id,
                Nome = f.Geral.Nome
            }).ToList();
            ViewData["usuario_id"] = new SelectList(usuarios, "Id", "Nome");
            var items = _context.item
                .Where(u => u.situacao == "1" && u.estoque_id == 1)
                .Select(u => new SelectListItem
                {
                    Value = u.id.ToString(),
                    Text = u.descricao
                })
                .ToList();
            ViewData["item_id"] = new SelectList(items, "Value", "Text");
            return View();
        }

        // POST: Cadastro/MovimentacaoItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,item_id,usuario_id,data_retirada,previsao_devolvido,devolvido,descricao")] movimentacao movimentacao)
        {
            if (ModelState.IsValid)
            {
                if (movimentacao.devolvido != null)
                {
                    movimentacao.devolvido = TimeZoneInfo.ConvertTimeFromUtc(movimentacao.devolvido.Value, TimeZoneInfo.Local);
                }

                _context.Add(movimentacao);
                await _context.SaveChangesAsync();

                var item = await _context.item.FindAsync(movimentacao.item_id);
                if (item != null)
                {
                    item.situacao = "3";
                    _context.item.Update(item);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            var usuarios = _context.usuario.Where(u => u.Geral.Situacao == "1" )
                .Select(f => new
                {
                    Id = f.Id,
                    Nome = f.Geral.Nome
                }).ToList();
            ViewData["usuario_id"] = new SelectList(usuarios, "Id", "Nome");
            var items = _context.item
                .Where(u => u.situacao == "1" && u.estoque_id == 1)
                .Select(u => new SelectListItem
                {
                    Value = u.id.ToString(),
                    Text = u.descricao
                })
                .ToList();
            ViewData["item_id"] = new SelectList(items, "Value", "Text");
            return View(movimentacao);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentacao = await _context.movimentacao.FindAsync(id);
            if (movimentacao == null)
            {
                return NotFound();
            }

            // Verificar se a movimentação já foi devolvida
            if (movimentacao.devolvido != null)
            {
                TempData["Message"] = "Este item já foi devolvido.";
                return RedirectToAction(nameof(Index));
            }

            var usuarios = _context.usuario.Where(u => u.Geral.Situacao == "1" )
                .Select(f => new
                {
                    Id = f.Id,
                    Nome = f.Geral.Nome
                }).ToList();
            ViewData["usuario_id"] = new SelectList(usuarios, "Id", "Nome");
            var items = _context.item
                .Where(u => u.situacao == "1" && u.estoque_id == 1)
                .Select(u => new SelectListItem
                {
                    Value = u.id.ToString(),
                    Text = u.descricao
                })
                .ToList();
                var item = _context.item
                .Where(u => u.id == movimentacao.item_id)
                .Select(u => new SelectListItem
                {
                    Value = u.id.ToString(),
                    Text = u.descricao
                })
                .FirstOrDefault();
                if (item != null)
                    {
                        ViewData["item_id"] = new SelectList(new List<SelectListItem> { item }, "Value", "Text");
                    }
            return View(movimentacao);
        }


        // POST: Cadastro/MovimentacaoItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,item_id,usuario_id,data_retirada,previsao_devolvido,devolvido,descricao")] movimentacao movimentacao)
        {
            if (id != movimentacao.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (movimentacao.devolvido != null)
                    {
                        movimentacao.devolvido = DateTime.SpecifyKind(movimentacao.devolvido.Value, DateTimeKind.Utc);
                        var item = await _context.item.FindAsync(movimentacao.item_id);
                        if (item != null)
                        {
                            item.situacao = "1";
                            _context.item.Update(item);
                            await _context.SaveChangesAsync();
                        }
                    }


                    _context.Update(movimentacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!movimentacaoExists(movimentacao.id))
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
            var usuarios = _context.usuario.Where(u => u.Geral.Situacao == "1" )
            .Select(f => new
            {
                Id = f.Id,
                Nome = f.Geral.Nome
            }).ToList();
            ViewData["usuario_id"] = new SelectList(usuarios, "Id", "Nome");
            var items = _context.item
                .Where(u => u.situacao == "1" && u.estoque_id == 1)
                .Select(u => new SelectListItem
                {
                    Value = u.id.ToString(),
                    Text = u.descricao
                })
                .ToList();
            ViewData["item_id"] = new SelectList(items, "Value", "Text");
            return View(movimentacao);
        }

        // GET: Cadastro/MovimentacaoItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentacao = await _context.movimentacao
                .Include(m => m.item)
                .Include(m => m.usuario)
                .ThenInclude(d => d.Geral)
                .FirstOrDefaultAsync(m => m.id == id);
            if (movimentacao == null)
            {
                return NotFound();
            }

            return View(movimentacao);
        }

        // POST: Cadastro/MovimentacaoItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movimentacao = await _context.movimentacao.FindAsync(id);
            _context.movimentacao.Remove(movimentacao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool movimentacaoExists(int id)
        {
            return _context.movimentacao.Any(e => e.id == id);
        }
    }
}
