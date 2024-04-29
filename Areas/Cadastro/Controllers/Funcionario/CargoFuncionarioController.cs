using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EspacoPotencial.Areas.Cadastro.Models.Funcionarios;
using EspacoPotencial.Context;
using EspacoPotencial.Filters;
using EspacoPotencial.Models.Account;

namespace EspacoPotencial.Areas.Cadastro.Controllers.Funcionario
{
    [Area("Cadastro")]
    [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
    public class CargoFuncionarioController : Controller
    {
        private readonly ApaDbContext _context;

        public CargoFuncionarioController(ApaDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro/CargoFuncionario
        [Autorizacao(new[] { TipoUsuario.SuperUser , TipoUsuario.Admin})]
        public async Task<IActionResult> Index()
        {
           var apaDbContext = _context.cargo_funcionario
            .Include(c => c.motivo_desligamento)
            .Include(c => c.cargos)
            .Include(c => c.funcionario)
                .ThenInclude(fu => fu.geral)
            .AsNoTracking();

            return View(await apaDbContext.ToListAsync());
        }

        // GET: Cadastro/CargoFuncionario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargo_funcionario = await _context.cargo_funcionario
            .Include(c => c.motivo_desligamento)
            .Include(c => c.cargos)
            .Include(c => c.funcionario)
                .ThenInclude(fu => fu.geral)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cargo_funcionario == null)
            {
                return NotFound();
            }

            return View(cargo_funcionario);
        }

        // GET: Cadastro/CargoFuncionario/Create
        public IActionResult Create()
        {
            
            ViewData["cargos_id"] = new SelectList(_context.cargos, "Id", "Nome");

            var motivosDesligamento = _context.motivo_desligamento.ToList();
            motivosDesligamento.Insert(0, new motivo_desligamento { Id = 0, Descricao = "" });
            ViewData["motivo_id"] = new SelectList(motivosDesligamento, "Id", "Descricao");

           var funcionarios = _context.funcionario.Where(f => f.geral.Tipo == "1")
            .Select(f => new
            {
                Id = f.Id,
                Nome = f.geral.Nome
            }).ToList();

            ViewData["funcionario_nome"] = new SelectList(funcionarios, "Id", "Nome");
    
            return View();
        }

        // POST: Cadastro/CargoFuncionario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,funcionario_id,cargos_id,Situacao,motivo_id,DataInicial,DataFinal,Observacao")] cargo_funcionario cargo_funcionario)
        {
            if (ModelState.IsValid)
            {   
                if (cargo_funcionario.DataFinal != null)
                {
                    cargo_funcionario.DataFinal = cargo_funcionario.DataFinal.Value;
                }
                if(cargo_funcionario.motivo_id == 0){

                    cargo_funcionario.motivo_id = null;
                }
                _context.Add(cargo_funcionario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
            
            return View(cargo_funcionario);
        }

        // GET: Cadastro/CargoFuncionario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargo_funcionario = await _context.cargo_funcionario.FindAsync(id);
            int funcionarioId = cargo_funcionario.funcionario_id;
            if (cargo_funcionario == null)
            {
                return NotFound();
            }

           ViewData["cargos_id"] = new SelectList(_context.cargos, "Id", "Nome");

            var motivosDesligamento = _context.motivo_desligamento.ToList();
            motivosDesligamento.Insert(0, new motivo_desligamento { Id = 0, Descricao = "" });
            ViewData["motivo_id"] = new SelectList(motivosDesligamento, "Id", "Descricao");

           var funcionarios = _context.funcionario.Where(f => f.geral.Tipo == "1")
            .Select(f => new
            {
                Id = f.Id,
                Nome = f.geral.Nome
            }).ToList();

            ViewData["funcionario_nome"] = new SelectList(funcionarios, "Id", "Nome");

            return View(cargo_funcionario);
        }

        // POST: Cadastro/CargoFuncionario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,funcionario_id,cargo_id,Situacao,motivo_id,DataInicial,DataFinal,Observacao")] cargo_funcionario cargo_funcionario)
        {
            if (id != cargo_funcionario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(cargo_funcionario.motivo_id == 0){
                        cargo_funcionario.motivo_id = null;
                    }

                    _context.Update(cargo_funcionario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!cargo_funcionarioExists(cargo_funcionario.Id))
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
            ViewData["cargos_id"] = new SelectList(_context.cargos, "Id", "Nome");

            var motivosDesligamento = _context.motivo_desligamento.ToList();
            motivosDesligamento.Insert(0, new motivo_desligamento { Id = 0, Descricao = "" });
            ViewData["motivo_id"] = new SelectList(motivosDesligamento, "Id", "Descricao");

           var funcionarios = _context.funcionario.Where(f => f.geral.Tipo == "1")
            .Select(f => new
            {
                Id = f.Id,
                Nome = f.geral.Nome
            }).ToList();

            ViewData["funcionario_nome"] = new SelectList(funcionarios, "Id", "Nome");
            return View(cargo_funcionario);
        }

        // GET: Cadastro/CargoFuncionario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargo_funcionario = await _context.cargo_funcionario
                .Include(c => c.motivo_desligamento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cargo_funcionario == null)
            {
                return NotFound();
            }

            return View(cargo_funcionario);
        }

        // POST: Cadastro/CargoFuncionario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cargo_funcionario = await _context.cargo_funcionario.FindAsync(id);
            _context.cargo_funcionario.Remove(cargo_funcionario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool cargo_funcionarioExists(int id)
        {
            return _context.cargo_funcionario.Any(e => e.Id == id);
        }

        
    }
}
