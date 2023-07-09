using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DeliveryEat.Data;
using DeliveryEat.Models;
using Microsoft.AspNetCore.Identity;
using static DeliveryEat.Models.ViewModel;


namespace DeliveryEat.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public PessoasAPIController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        // GET: api/PessoasAPI
        [HttpGet]

        public async Task<ActionResult<IEnumerable<PessoaViewModel>>> GetPessoas()
        {
            return await _context.Pessoas.OrderBy(p => p.Nome).Select(p => new PessoaViewModel
            {
                Id = p.Id,
                Nome = p.Nome
            }).ToListAsync();
        }


        // GET: api/PessoasAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pessoa>> GetPessoa(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);

            if (pessoa == null)
            {
                return NotFound();
            }

            return pessoa;
        }

        // PUT: api/PessoasAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPessoa(int id, Pessoa pessoa)
        {
            if (id != pessoa.Id)
            {
                return BadRequest();
            }

            _context.Entry(pessoa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoaExists(id))
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

        // POST: api/PessoasAPI/Register
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPost("create")]

        public async Task<ActionResult<Pessoa>> PostPessoa(Pessoa pessoa)
        {
            //cria um Utilizador com as respetivas informaçõs fornecidas
            var user = new ApplicationUser
            {
                UserName = pessoa.Email,
                Email = pessoa.Email,
                Nome = pessoa.Nome,
                DataRegisto = DateTime.Now,
                EmailConfirmed = true
            };

            string password = pessoa.Password;

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Cliente");
                pessoa.UserId = user.Id;

                try
                {
                    _context.Pessoas.Add(pessoa);
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    _context.Pessoas.Remove(pessoa);
                    await _context.SaveChangesAsync();
                }

                return CreatedAtAction("GetPessoa", new { id = pessoa.Id }, pessoa);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        // POST: api/PessoasAPI/Login
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);


            if (user == null)
            {
                return BadRequest("Email inválido");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return BadRequest("Password inválida");
            }

            var pessoa = await _context.Pessoas.FirstOrDefaultAsync(i => i.UserId == user.Id);

            return Ok(new { PessoaID = pessoa.Id });
        }

        // POST: api/PessoasAPI/Logout
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Ok(new { Message = "Saiu com sucesso" });
        }


        // DELETE: api/PessoasAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePessoa(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }

            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PessoaExists(int id)
        {
            return _context.Pessoas.Any(e => e.Id == id);
        }
    }
}
