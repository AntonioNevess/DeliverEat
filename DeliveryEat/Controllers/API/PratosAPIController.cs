using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DeliveryEat.Data;
using DeliveryEat.Models;
using static DeliveryEat.Models.ViewModel;

namespace DeliveryEat.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PratosAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PratosAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PratosAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PratoViewModel>>> GetPratos(int? restauranteId)
        {
            if (restauranteId == null)
            {
                return BadRequest("Restaurante ID is required.");
            }

            // obtem o restaunrante atual
            var restaurante = await _context.Restaurantes.FindAsync(restauranteId);
            if (restaurante == null)
            { 
                return NotFound("Restaurante not found.");
            }

            // obtrem os pratos do restaurante
            var pratos = await _context.Pratos
                .Where(p => p.RestauranteFK == restauranteId)
                .Include(p => p.Restaurante)
                .Select(p => new PratoViewModel
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Restaurante = p.Restaurante.Nome,
                    Preco = p.Preco,    
                })
                .ToListAsync();

            return pratos;
        }

        // GET: api/PratosAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PratoViewModel>> GetPrato(int id)
        {
            var prato = await _context.Pratos.Include(p => p.Restaurante)
                .Where(p => p.Id == id)
                .Select(p => new PratoViewModel
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Restaurante = p.Restaurante.Nome
                })
                .FirstOrDefaultAsync();

            if (prato == null)
            {
                return NotFound("Prato not found.");
            }

            return prato;
        }

        // PUT: api/PratosAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrato(int id, Prato prato)
        {
            if (id != prato.Id)
            {
                return BadRequest();
            }

            _context.Entry(prato).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PratoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PratosAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Prato>> PostPrato(Prato prato)
        {
            _context.Pratos.Add(prato);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrato", new { id = prato.Id }, prato);
        }

        // DELETE: api/PratosAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrato(int id)
        {
            var prato = await _context.Pratos.FindAsync(id);
            if (prato == null)
            {
                return NotFound();
            }

            _context.Pratos.Remove(prato);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PratoExists(int id)
        {
            return _context.Pratos.Any(e => e.Id == id);
        }
    }
}
