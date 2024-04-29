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
    [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin, TipoUsuario.Funcionarios})]
    public class ResponsavelController : Controller
    {
        private readonly ApaDbContext _context;

        public ResponsavelController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/Responsavel
        public async Task<IActionResult> Index()
        {
            var apaDbContext = _context.responsavel
            .Include(r => r.Geral)
            .Include(r => r.Usuario)
            .Include(r => r.Funcionario)
            .ThenInclude(f => f.geral) // Inclui os dados do funcionário
            .Where(r => r.Geral.Tipo == "2" || r.Geral.Tipo == "3");
            return View(await apaDbContext.ToListAsync());
        }

        // GET: Cadastro/Responsavel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responsavel = await _context.responsavel
                .Include(r => r.Geral)
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (responsavel == null)
            {
                return NotFound();
            }

            return View(responsavel);
        }

        // GET: Cadastro/Responsavel/Create
        public IActionResult Create()
        {
            var usuarios = _context.usuario.Where(u => u.Geral.Tipo == "2")
            .Select(u => new{
                Id = u.Id,
                Nome = u.Geral.Nome
            }).ToList();

            ViewData["usuarios"] = new SelectList(usuarios, "Id", "Nome");

            var funcionarios = _context.funcionario.Where(u => u.geral.Tipo == "3")
            .Select(u => new{
                Id = u.Id,
                Nome = u.geral.Nome
            }).ToList();

            ViewData["funcionarios"] = new SelectList(funcionarios, "Id", "Nome");
            
            return View();
        }

        // POST: Cadastro/Responsavel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("geral_id,geral_func,usuario_id,funcionario_id,Vinculo,LocalTrabalho,Retira,Observacao")] responsavel responsavel)
        {
            
            var IdGeral = await _context.usuario.Where(g => g.Id == responsavel.usuario_id).Select(g => g.geral_id).FirstOrDefaultAsync();
            responsavel.geral_id = IdGeral; 

            var GeralFunc =  await _context.funcionario.Where(g => g.Id == responsavel.funcionario_id).Select(g => g.geral_id).FirstOrDefaultAsync();
            responsavel.geral_func = GeralFunc; 
            if (ModelState.IsValid)
            {
                _context.Add(responsavel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
             var usuarios = _context.usuario.Where(u => u.Geral.Tipo == "2")
            .Select(u => new{
                Id = u.Id,
                Nome = u.Geral.Nome
            }).ToList();

            ViewData["usuarios"] = new SelectList(usuarios, "Id", "Nome");

            var funcionarios = _context.funcionario.Where(u => u.geral.Tipo == "3")
            .Select(u => new{
                Id = u.Id,
                Nome = u.geral.Nome
            }).ToList();

            ViewData["funcionarios"] = new SelectList(funcionarios, "Id", "Nome");


            return View(responsavel);
        }

        // GET: Cadastro/Responsavel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responsavel = await _context.responsavel.FindAsync(id);
            if (responsavel == null)
            {
                return NotFound();
            }
            var usuarios = _context.usuario.Where(u => u.Geral.Tipo == "2")
            .Select(u => new{
                Id = u.Id,
                Nome = u.Geral.Nome
            }).ToList();

            ViewData["usuarios"] = new SelectList(usuarios, "Id", "Nome");

            var funcionarios = _context.funcionario.Where(u => u.geral.Tipo == "3")
            .Select(u => new{
                Id = u.Id,
                Nome = u.geral.Nome
            }).ToList();

            ViewData["funcionarios"] = new SelectList(funcionarios, "Id", "Nome");
            return View(responsavel);
        }

        // POST: Cadastro/Responsavel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,geral_id,usuario_id,Vinculo,LocalTrabalho,Retira,Observacao")] responsavel responsavel)
        {
            if (id != responsavel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                     var IdGeral = await _context.usuario.Where(g => g.Id == responsavel.usuario_id).Select(g => g.geral_id).FirstOrDefaultAsync();
            responsavel.geral_id = IdGeral; 

            var GeralFunc =  await _context.funcionario.Where(g => g.Id == responsavel.funcionario_id).Select(g => g.geral_id).FirstOrDefaultAsync();
            responsavel.geral_func = GeralFunc; 
                    _context.Update(responsavel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!responsavelExists(responsavel.Id))
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
            var usuarios = _context.usuario.Where(u => u.Geral.Tipo == "2")
            .Select(u => new{
                Id = u.Id,
                Nome = u.Geral.Nome
            }).ToList();

            ViewData["usuarios"] = new SelectList(usuarios, "Id", "Nome");

            var funcionarios = _context.funcionario.Where(u => u.geral.Tipo == "3")
            .Select(u => new{
                Id = u.Id,
                Nome = u.geral.Nome
            }).ToList();

            ViewData["funcionarios"] = new SelectList(funcionarios, "Id", "Nome");
            return View(responsavel);
        }

        // GET: Cadastro/Responsavel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responsavel = await _context.responsavel
                .Include(r => r.Geral)
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (responsavel == null)
            {
                return NotFound();
            }

            return View(responsavel);
        }

        // POST: Cadastro/Responsavel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var responsavel = await _context.responsavel.FindAsync(id);
            _context.responsavel.Remove(responsavel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool responsavelExists(int id)
        {
            return _context.responsavel.Any(e => e.Id == id);
        }
    }
}
