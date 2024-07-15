using Microsoft.EntityFrameworkCore;
using Warlock.Models;

namespace Warlock.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Spell", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Potion", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Weapon", DisplayOrder = 3 }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
