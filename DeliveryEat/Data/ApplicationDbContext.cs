using DeliveryEat.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DeliveryEat.Data
{
    /// <summary>
    /// This class represents the Database of our project.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        public DateTime DataRegisto { get; set; }
        public string Nome { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Create data for roles
            modelBuilder.Entity<IdentityRole>().HasData(
               new IdentityRole { Id = "1", Name = "Restaurante", NormalizedName = "Restaurante" },
               new IdentityRole { Id = "2", Name = "Administrativo", NormalizedName = "ADMINISTRATIVO" },
               new IdentityRole { Id = "3", Name = "Cliente", NormalizedName = "CLIENTE" }
            );
        }

        public DbSet<Restaurante> Restaurantes { get; set; }
        public DbSet<Prato> Pratos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<DetalhesPedido> DetalhesPedidos { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
    }
}
