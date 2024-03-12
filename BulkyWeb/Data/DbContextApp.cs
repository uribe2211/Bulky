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
    }
}
