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
    public class PedidosAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PedidosAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PedidosAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewModel.PedidoViewModel>>> GetPedidos()
        {
            var pedidos = await _context.Pedidos.ToListAsync();
            var pedidoViewModels = pedidos.Select(p => new ViewModel.PedidoViewModel
            {
                Id = p.Id,
                Confirmed = p.Confirmed
            }).ToList();

            return pedidoViewModels;
        }

        // GET: api/PedidosAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ViewModel.PedidoViewModel>> GetPedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido == null)
            {
                return NotFound();
            }

            var pedidoViewModel = new ViewModel.PedidoViewModel
            {
                Id = pedido.Id,
                Confirmed = pedido.Confirmed
            };

            return pedidoViewModel;
        }

        // PUT: api/PedidosAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(int id, ViewModel.PedidoViewModel pedidoViewModel)
        {
            if (id != pedidoViewModel.Id)
            {
                return BadRequest();
            }

            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            pedido.Confirmed = pedidoViewModel.Confirmed;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(id))
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

        // POST: api/PedidosAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ViewModel.PedidoViewModel>> PostPedido(ViewModel.PedidoViewModel pedidoViewModel)
        {
            var pedido = new Pedido
            {
                Confirmed = pedidoViewModel.Confirmed
            };

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            pedidoViewModel.Id = pedido.Id;

            return CreatedAtAction("GetPedido", new { id = pedido.Id }, pedidoViewModel);
        }

        // DELETE: api/PedidosAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.Id == id);
        }
    }
}
