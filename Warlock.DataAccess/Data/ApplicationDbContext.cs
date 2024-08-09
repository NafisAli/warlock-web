using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Warlock.Models;

namespace Warlock.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Faction> Factions { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Category>()
                .HasData(
                    new Category
                    {
                        Id = 1,
                        Name = "Spell",
                        DisplayOrder = 1
                    },
                    new Category
                    {
                        Id = 2,
                        Name = "Potion",
                        DisplayOrder = 2
                    },
                    new Category
                    {
                        Id = 3,
                        Name = "Weapon",
                        DisplayOrder = 3
                    }
                );

            modelBuilder
                .Entity<Faction>()
                .HasData(
                    new Faction
                    {
                        Id = 1,
                        Name = "Traders",
                        StreetAddress = "123 Faction St",
                        City = "Faction City",
                        State = "Faction State",
                        PostCode = "1234",
                        PhoneNumber = "0123456789"
                    },
                    new Faction
                    {
                        Id = 2,
                        Name = "Explorers",
                        StreetAddress = "123 Faction St",
                        City = "Faction City",
                        State = "Faction State",
                        PostCode = "1234",
                        PhoneNumber = "0123456789"
                    },
                    new Faction
                    {
                        Id = 3,
                        Name = "Mercenaries",
                        StreetAddress = "123 Faction St",
                        City = "Faction City",
                        State = "Faction State",
                        PostCode = "1234",
                        PhoneNumber = "0123456789"
                    }
                );

            modelBuilder
                .Entity<Product>()
                .HasData(
                    new Product
                    {
                        Id = 1,
                        Title = "Fireball",
                        Level = 1,
                        Description =
                            "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                        Tier = "Common",
                        ListPrice = 99,
                        Price = 90,
                        Price50 = 85,
                        Price100 = 80,
                        CategoryId = 1
                    },
                    new Product
                    {
                        Id = 2,
                        Title = "Healing Potion",
                        Level = 2,
                        Description =
                            "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                        Tier = "Normal",
                        ListPrice = 40,
                        Price = 30,
                        Price50 = 25,
                        Price100 = 20,
                        CategoryId = 2
                    },
                    new Product
                    {
                        Id = 3,
                        Title = "Wooden Staff",
                        Level = 3,
                        Description =
                            "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                        Tier = "Normal",
                        ListPrice = 55,
                        Price = 50,
                        Price50 = 40,
                        Price100 = 35,
                        CategoryId = 3
                    },
                    new Product
                    {
                        Id = 4,
                        Title = "Steel Dagger",
                        Level = 4,
                        Description =
                            "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                        Tier = "Epic",
                        ListPrice = 70,
                        Price = 65,
                        Price50 = 60,
                        Price100 = 55,
                        CategoryId = 3
                    },
                    new Product
                    {
                        Id = 5,
                        Title = "Rock in the Ocean",
                        Level = 5,
                        Description =
                            "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                        Tier = "Legendary",
                        ListPrice = 300,
                        Price = 270,
                        Price50 = 250,
                        Price100 = 200,
                        CategoryId = 3
                    },
                    new Product
                    {
                        Id = 6,
                        Title = "Leaves and Wonders",
                        Level = 6,
                        Description =
                            "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                        Tier = "Legendary",
                        ListPrice = 250,
                        Price = 230,
                        Price50 = 220,
                        Price100 = 200,
                        CategoryId = 3
                    }
                );
        }
    }
}
