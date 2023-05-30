using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DeliveryEat.Data;
using DeliveryEat.Models;

namespace DeliveryEat.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalhesPedidosAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DetalhesPedidosAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/DetalhesPedidosAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalhesPedido>>> GetDetalhesPedidos()
        {
            return await _context.DetalhesPedidos.ToListAsync();
        }

        // GET: api/DetalhesPedidosAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalhesPedido>> GetDetalhesPedido(int id)
        {
            var detalhesPedido = await _context.DetalhesPedidos.FindAsync(id);

            if (detalhesPedido == null)
            {
                return NotFound();
            }

            return detalhesPedido;
        }

        // PUT: api/DetalhesPedidosAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalhesPedido(int id, DetalhesPedido detalhesPedido)
        {
            if (id != detalhesPedido.Id)
            {
                return BadRequest();
            }

            _context.Entry(detalhesPedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalhesPedidoExists(id))
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

        // POST: api/DetalhesPedidosAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetalhesPedido>> PostDetalhesPedido(DetalhesPedido detalhesPedido)
        {
            _context.DetalhesPedidos.Add(detalhesPedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetalhesPedido", new { id = detalhesPedido.Id }, detalhesPedido);
        }

        // DELETE: api/DetalhesPedidosAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalhesPedido(int id)
        {
            var detalhesPedido = await _context.DetalhesPedidos.FindAsync(id);
            if (detalhesPedido == null)
            {
                return NotFound();
            }

            _context.DetalhesPedidos.Remove(detalhesPedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalhesPedidoExists(int id)
        {
            return _context.DetalhesPedidos.Any(e => e.Id == id);
        }
    }
}
