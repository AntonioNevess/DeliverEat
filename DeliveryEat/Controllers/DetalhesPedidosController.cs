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
using System.Diagnostics.Contracts;

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
            // Obtem o utilizador atual
            var user = await _userManager.GetUserAsync(User);

            var pessoa = await _context.Pessoas.FirstOrDefaultAsync(p => p.UserId == user.Id);


            // obtem os detalhes pedidos associados aos pedidos
            var cartItems = await _context.DetalhesPedidos
                .Where(d => d.PedidosFK == id)
                .Include(d => d.Pratos)
                .ToListAsync();

            return View(cartItems);
        }


        /// <summary>
        /// esta função serve para adicionarpratos do restaurnte ao "carrinho"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

            // obter o utilizador 
            var user = await _userManager.GetUserAsync(User);

            var pessoa = await _context.Pessoas.FirstOrDefaultAsync(p => p.UserId == user.Id);

            // verificar se existe pedidos relacionados ao utilizador 
            var confirmedPedido = await _context.Pedidos
                                        .Where(p => p.PessoaFK == pessoa.Id)
                                        .FirstOrDefaultAsync();

            Pedido pedido; 

            if (confirmedPedido != null && confirmedPedido.Confirmed)
            {
                // cria um novo pedido caso o confirmedPedido nao seja null e seja true
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
                //se o pedido for null cria um novo pedido
                if (pedido == null)
                {
                    pedido = new Pedido
                    {
                        PessoaFK = pessoa.Id,
                    };

                    _context.Pedidos.Add(pedido);
                    await _context.SaveChangesAsync();
                }
            }

            //cria um novo detalhePedido
            var detalhesPedido = new DetalhesPedido
            {
                PratoFK = prato.Id,
                NomePrato = prato.Nome,
                Quantidade = 1,
                Preco = prato.Preco,
                PedidosFK = pedido.Id 
            };

            //adiciona à db
            _context.DetalhesPedidos.Add(detalhesPedido);
            await _context.SaveChangesAsync();

            // adiciona a listadetalhesPedido
            pedido.ListaDetalhesPedido.Add(detalhesPedido);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index","Restaurantes");
        }


        public async Task<IActionResult> ConfirmCart()
        {
            var user = await _userManager.GetUserAsync(User);

            var pessoa = await _context.Pessoas.FirstOrDefaultAsync(p => p.UserId == user.Id);

            // obtem o pedido da pessoa atual
            var pedido = await _context.Pedidos.FirstOrDefaultAsync(p => p.PessoaFK == pessoa.Id);

            if (pedido == null)
            {
                return NotFound();
            }

            //mete flag a verdadeiro
            pedido.Confirmed = true; 
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