using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeliveryEat.Data;
using DeliveryEat.Models;

namespace DeliveryEat.Controllers
{
    public class DetalhesPedidosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DetalhesPedidosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DetalhesPedidos
        public async Task<IActionResult> Index()
        {

              return View(await _context.DetalhesPedidos.ToListAsync());
        }

        // GET: DetalhesPedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DetalhesPedidos == null)
            {
                return NotFound();
            }

            var detalhesPedido = await _context.DetalhesPedidos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detalhesPedido == null)
            {
                return NotFound();
            }

            return View(detalhesPedido);
        }

        // GET: DetalhesPedidos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DetalhesPedidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomePrato,Quantidade,Preco,PrecoPedidoAux")] DetalhesPedido detalhesPedido)
        {
            if (!string.IsNullOrEmpty(detalhesPedido.PrecoPedidoAux)) {
                detalhesPedido.Preco = Convert.ToDecimal(detalhesPedido.PrecoPedidoAux.Replace('.', ','));
            }

            if (ModelState.IsValid)
            {
                _context.Add(detalhesPedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(detalhesPedido);
        }

        // GET: DetalhesPedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DetalhesPedidos == null)
            {
                return NotFound();
            }

            var detalhesPedido = await _context.DetalhesPedidos.FindAsync(id);
            if (detalhesPedido == null)
            {
                return NotFound();
            }
            return View(detalhesPedido);
        }

        // POST: DetalhesPedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomePrato,Quantidade,Preco")] DetalhesPedido detalhesPedido)
        {
            if (id != detalhesPedido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalhesPedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalhesPedidoExists(detalhesPedido.Id))
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
            return View(detalhesPedido);
        }

        // GET: DetalhesPedidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DetalhesPedidos == null)
            {
                return NotFound();
            }

            var detalhesPedido = await _context.DetalhesPedidos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detalhesPedido == null)
            {
                return NotFound();
            }

            return View(detalhesPedido);
        }

        // POST: DetalhesPedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DetalhesPedidos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DetalhesPedidos'  is null.");
            }
            var detalhesPedido = await _context.DetalhesPedidos.FindAsync(id);
            if (detalhesPedido != null)
            {
                _context.DetalhesPedidos.Remove(detalhesPedido);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalhesPedidoExists(int id)
        {
          return _context.DetalhesPedidos.Any(e => e.Id == id);
        }
    }
}
