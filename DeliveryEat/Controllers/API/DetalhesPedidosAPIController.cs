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
        public async Task<ActionResult<IEnumerable<ViewModel.DetalhesPedidoModel>>> GetDetalhesPedidos()
        {
            var detalhesPedidos = await _context.DetalhesPedidos.ToListAsync();
            var detalhesPedidoModels = detalhesPedidos.Select(d => new ViewModel.DetalhesPedidoModel
            {
                Id = d.Id,
                NomePrato = d.NomePrato,
                Quantidade = d.Quantidade,
                Preco = d.Preco
            }).ToList();

            return detalhesPedidoModels;
        }

        // GET: api/DetalhesPedidosAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ViewModel.DetalhesPedidoModel>> GetDetalhesPedido(int id)
        {
            var detalhesPedido = await _context.DetalhesPedidos.FindAsync(id);

            if (detalhesPedido == null)
            {
                return NotFound();
            }

            var detalhesPedidoModel = new ViewModel.DetalhesPedidoModel
            {
                Id = detalhesPedido.Id,
                NomePrato = detalhesPedido.NomePrato,
                Quantidade = detalhesPedido.Quantidade,
                Preco = detalhesPedido.Preco
            };

            return detalhesPedidoModel;
        }

        // PUT: api/DetalhesPedidosAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalhesPedido(int id, ViewModel.DetalhesPedidoModel detalhesPedidoModel)
        {
            if (id != detalhesPedidoModel.Id)
            {
                return BadRequest();
            }

            var detalhesPedido = await _context.DetalhesPedidos.FindAsync(id);
            if (detalhesPedido == null)
            {
                return NotFound();
            }

            detalhesPedido.NomePrato = detalhesPedidoModel.NomePrato;
            detalhesPedido.Quantidade = detalhesPedidoModel.Quantidade;
            detalhesPedido.Preco = detalhesPedidoModel.Preco;

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
        [HttpPost("Create")]
        public async Task<ActionResult<ViewModel.DetalhesPedidoModel>> PostDetalhesPedido(ViewModel.DetalhesPedidoModel detalhesPedidoModel)
        {
            // Check if the referenced Pedido exists
            var pedido = await _context.Pedidos.FindAsync(detalhesPedidoModel.Id);
            if (pedido == null)
            {
                return NotFound("Pedido not found");
            }

            var detalhesPedido = new DetalhesPedido
            {
                NomePrato = detalhesPedidoModel.NomePrato,
                Quantidade = detalhesPedidoModel.Quantidade,
                Preco = detalhesPedidoModel.Preco,
                PedidosFK = pedido.Id // Set the Pedido navigation property
            };

            _context.DetalhesPedidos.Add(detalhesPedido);
            await _context.SaveChangesAsync();

            detalhesPedidoModel.Id = detalhesPedido.Id;
            return CreatedAtAction("GetDetalhesPedido", new { id = detalhesPedido.Id }, detalhesPedidoModel);
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
