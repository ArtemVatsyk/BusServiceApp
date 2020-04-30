using BusServiceApp.ReceiverMvcApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BusServiceApp.ReceiverMvcApp.Models
{
    public sealed class UserContext : DbContext
    {

        public DbSet<User> Users { get; set; }

        public UserContext()
        {
             Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=busreceivedb;Trusted_Connection=True;");
        }
    }
}