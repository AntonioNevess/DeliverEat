using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeliveryEat.Data;
using DeliveryEat.Models;
using Microsoft.AspNetCore.Identity;

namespace DeliveryEat.Controllers
{
    public class DetalhesPedidosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DetalhesPedidosController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: DetalhesPedidos
        public async Task<IActionResult> Index(int id)
        {
            // Get the current user
            var user = await _userManager.GetUserAsync(User);

            // Get the Pessoa associated with the current user
            var pessoa = await _context.Pessoas.FirstOrDefaultAsync(p => p.UserId == user.Id);

            // Get the Pedidos associated with the current Pessoa
            /*var pedidos = await _context.Pedidos
                .Where(p => p.PessoaFK == pessoa.Id)
                .Select(p => p.Id)
                .ToListAsync();*/

            // Get the DetalhesPedidos associated with the current Pedidos
            /* var cartItems = await _context.DetalhesPedidos
                 .Where(d => pedidos.Contains(d.PedidosFK))
                 .Include(d => d.Pratos)
                 .ToListAsync();*/

            // Get the DetalhesPedidos associated with the current Pedidos
            var cartItems = await _context.DetalhesPedidos
                .Where(d => d.PedidosFK == id)
                .Include(d => d.Pratos)
                .ToListAsync();

            return View(cartItems);
        }


        public async Task<IActionResult> AddToCart(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prato = await _context.Pratos.FindAsync(id);
            if (prato == null)
            {
                return NotFound();
            }

            // Get the current user
            var user = await _userManager.GetUserAsync(User);

            // Get the Pessoa associated with the current user
            var pessoa = await _context.Pessoas.FirstOrDefaultAsync(p => p.UserId == user.Id);

            // Check if a confirmed Pedido exists for the current user
            var confirmedPedido = await _context.Pedidos
                                        .Where(p => p.PessoaFK == pessoa.Id)
                                        .FirstOrDefaultAsync();
            Pedido pedido; // Declare the 'pedido' variable

            if (confirmedPedido != null && confirmedPedido.Confirmed)
            {
                // Create a new Pedido since the previous one is confirmed
                pedido = new Pedido
                {
                    PessoaFK = pessoa.Id,
                };
                confirmedPedido.Confirmed = false;
                _context.Pedidos.Add(pedido);
                await _context.SaveChangesAsync();
            }
            else
            {
                pedido = await _context.Pedidos.FirstOrDefaultAsync(p => p.PessoaFK == pessoa.Id);
                if (pedido == null)
                {
                    // Create a new Pedido if it doesn't exist
                    pedido = new Pedido
                    {
                        PessoaFK = pessoa.Id,
                    };

                    _context.Pedidos.Add(pedido);
                    await _context.SaveChangesAsync(); // Save changes so that Pedido.Id is generated
                }
            }

            var detalhesPedido = new DetalhesPedido
            {
                PratoFK = prato.Id,
                NomePrato = prato.Nome,
                Quantidade = 1,
                Preco = prato.Preco,
                PedidosFK = pedido.Id // Use the Id of the Pedido we just retrieved or created
            };

            _context.DetalhesPedidos.Add(detalhesPedido);
            await _context.SaveChangesAsync();

            // Add the DetalhesPedido to the ListaDetalhesPedido of the Pedido
            pedido.ListaDetalhesPedido.Add(detalhesPedido);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index","Restaurantes");
        }


        public async Task<IActionResult> ConfirmCart()
        {
            // Get the current user
            var user = await _userManager.GetUserAsync(User);

            // Get the Pessoa associated with the current user
            var pessoa = await _context.Pessoas.FirstOrDefaultAsync(p => p.UserId == user.Id);

            // Get the Pedido associated with the current Pessoa
            var pedido = await _context.Pedidos.FirstOrDefaultAsync(p => p.PessoaFK == pessoa.Id);

            if (pedido == null)
            {
                return NotFound();
            }

            pedido.Confirmed = true; // Set the "Confirmed" flag to true
            await _context.SaveChangesAsync();

            return RedirectToAction("Index","Restaurantes");
        }


        // GET: DetalhesPedidos/RemoveFromCart/5
        public async Task<IActionResult> RemoveFromCart(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalhesPedido = await _context.DetalhesPedidos.FindAsync(id);
            if (detalhesPedido == null)
            {
                return NotFound();
            }

            _context.DetalhesPedidos.Remove(detalhesPedido);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: DetalhesPedidos/ClearCart
        public async Task<IActionResult> ClearCart()
        {
            var cartItems = await _context.DetalhesPedidos.ToListAsync();
            _context.DetalhesPedidos.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}