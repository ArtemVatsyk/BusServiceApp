using Microsoft.EntityFrameworkCore;

namespace BusServiceApp.EF.Models
{
    public sealed class ApplicationContext : DbContext
    {

        public DbSet<User> Users { get; set; }

        public ApplicationContext()
        {
           // Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=busservicedb;Trusted_Connection=True;");
        }
    }
}
