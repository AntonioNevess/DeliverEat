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
    public class PratosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PratosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pratos
        public async Task<IActionResult> Index(int? restauranteId)
        {
           
            if (restauranteId == null)
            {
                return NotFound();
            }

            // get do id do restaurante selecionado
            var restaurante = await _context.Restaurantes.FindAsync(restauranteId);
            if (restaurante == null)
            {
                return NotFound();
            }

            // get dos pratos do restaurnante selecionado
            var pratos = await _context.Pratos
                .Where(p => p.RestauranteFK == restauranteId)
                .ToListAsync();

            return View(pratos);
        }


        // GET: Pratos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pratos == null)
            {
                return NotFound();
            }

            var prato = await _context.Pratos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prato == null)
            {
                return NotFound();
            }

            return View(prato);
        }

        // GET: Pratos/Create
        public IActionResult Create()
        {
            ViewBag.RestauranteFK = new SelectList(_context.Restaurantes, "Id", "Nome");
            return View();
        }


        // POST: Pratos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descricao,Preco,PrecoPratoAux,RestauranteFK")] Prato prato)
        {
            if (!string.IsNullOrEmpty(prato.PrecoPratoAux))
            {
                prato.Preco = Convert.ToDecimal(prato.PrecoPratoAux.Replace('.', ','));
            }

            if (ModelState.IsValid)
            {
               //obtem o id do restaurante selecionado da dropdown
                int selectedRestauranteId = prato.RestauranteFK;

                // define a FK do restaurante para o respetivo id
                prato.RestauranteFK = selectedRestauranteId;

                _context.Add(prato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prato);
        }


        // GET: Pratos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pratos == null)
            {
                return NotFound();
            }

            var prato = await _context.Pratos.FindAsync(id);
            if (prato == null)
            {
                return NotFound();
            }
            return View(prato);
        }

        // POST: Pratos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,Preco")] Prato prato)
        {
            if (id != prato.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PratoExists(prato.Id))
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
            return View(prato);
        }

        // GET: Pratos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pratos == null)
            {
                return NotFound();
            }

            var prato = await _context.Pratos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prato == null)
            {
                return NotFound();
            }

            return View(prato);
        }

        // POST: Pratos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pratos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Pratos'  is null.");
            }
            var prato = await _context.Pratos.FindAsync(id);
            if (prato != null)
            {
                _context.Pratos.Remove(prato);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PratoExists(int id)
        {
          return _context.Pratos.Any(e => e.Id == id);
        }
    }
}
