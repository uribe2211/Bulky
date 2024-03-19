using BulkyWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Data
{
    public class DbContextApp : DbContext
    {
        public DbContextApp(DbContextOptions<DbContextApp>options):base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Drama", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Terror", DisplayOrder = 3 },
                new Category { Id = 4, Name = "SCFI", DisplayOrder = 4 },
                new Category { Id = 5, Name = "Comedia", DisplayOrder = 5 }
                );
        }
    }
}
