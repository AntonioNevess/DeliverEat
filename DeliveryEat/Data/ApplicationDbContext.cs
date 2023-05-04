using DeliveryEat.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DeliveryEat.Data
{
    /// <summary>
    /// esta classe representa a Base de Dados do nosso projeto
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /* *********************************************
        * Criação das Tabelas
        * ********************************************* */

        public DbSet<Restaurante> Restaurante { get; set; }
        public DbSet<Menu> Menu {get; set; }
        public DbSet<Pedido> Pedido { get; set; }  
        public DbSet<Utilizador> Utilizador { get; set;}

    }
}