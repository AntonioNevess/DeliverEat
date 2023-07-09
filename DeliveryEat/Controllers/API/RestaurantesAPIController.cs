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
    public class RestaurantesAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RestaurantesAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/RestaurantesAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestauranteViewModel>>> GetRestaurantes()
        {
            return await _context.Restaurantes.Select(r => new RestauranteViewModel
            {
                Id = r.Id,
                Nome = r.Nome,
                CP = r.CP
            }).ToListAsync();
        }

        // GET: api/RestaurantesAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RestauranteViewModel>> GetRestaurante(int id)
        {
            var restaurante = await _context.Restaurantes
                .Select(r => new RestauranteViewModel
                {
                    Id = r.Id,
                    Nome = r.Nome,
                    CP = r.CP
                }).Where(r => r.Id == id)
                .FirstOrDefaultAsync();


            if (restaurante == null)
            {
                return NotFound();
            }

            return restaurante;
        }

        // PUT: api/RestaurantesAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurante(int id, Restaurante restaurante)
        {
            if (id != restaurante.Id)
            {
                return BadRequest();
            }

            _context.Entry(restaurante).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestauranteExists(id))
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

        // POST: api/RestaurantesAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Restaurante>> PostRestaurante(Restaurante restaurante)
        {
            _context.Restaurantes.Add(restaurante);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRestaurante", new { id = restaurante.Id }, restaurante);
        }

        // DELETE: api/RestaurantesAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurante(int id)
        {
            var restaurante = await _context.Restaurantes.FindAsync(id);
            if (restaurante == null)
            {
                return NotFound();
            }

            _context.Restaurantes.Remove(restaurante);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RestauranteExists(int id)
        {
            return _context.Restaurantes.Any(e => e.Id == id);
        }
    }
}
